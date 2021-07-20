using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindML.Models;
using Northwinds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Data;
using Microsoft.ML.Trainers;
using System.IO;

namespace NorthwindML.Controllers
{
    public class HomeController : Controller
    {
        private readonly static string datasetName="dataset.txt";
        private readonly static string[] countries = new[]{"Germany","UK","USA"};


        private readonly ILogger<HomeController> _logger;
        private readonly Northwind db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, Northwind db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.db =db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var model = CreateHomeIndexViewModel();
            return View(model);
        }
        
        private string GetPath(string file){
            return Path.Combine(webHostEnvironment.ContentRootPath,"wwwroot","Data",file);
        }


        private HomeIndexViewModel CreateHomeIndexViewModel()
        {
            return new HomeIndexViewModel{
                Categories = db.Categories.Include(c => c.Products),
                GermanyDatasetExists = System.IO.File.Exists(GetPath("germany-dataset.txt")),
                UKDatasetExists = System.IO.File.Exists(GetPath("uk-dataset.txt")),
                USADatasetExists = System.IO.File.Exists(GetPath("usa-dataset.txt"))
            };
        }

        public IActionResult GenerateDataSets()
        {
            foreach (var country in countries)
            {
                IEnumerable<Order> orderInCountry = db.Orders
                    .Where(o => o.Customer.Country == country)
                    .Include(o => o.OrderDetails)
                    .AsEnumerable();
                
                IEnumerable<ProductCoBought> coBoughtProducts = orderInCountry.SelectMany(order =>
                    from lineItem1 in order.OrderDetails
                    from lineItem2 in order.OrderDetails
                    select new ProductCoBought
                    {
                        ProductID = (uint)lineItem1.ProductID,
                        CoBoughtProductID = (uint)lineItem2.ProductID
                    }
                ).Where(p => p.ProductID != p.CoBoughtProductID)
                .GroupBy(p => new {p.ProductID, p.CoBoughtProductID})
                .Select(p => p.FirstOrDefault())
                .OrderBy(p => p.ProductID)
                .ThenBy(p => p.CoBoughtProductID);

                StreamWriter datasetFile = System.IO.File.CreateText(path: GetPath($"{country.ToLower()}-{datasetName}"));

                datasetFile.WriteLine("ProductID\tCoBoughtProductID");

                foreach (var item in coBoughtProducts)
                {
                    datasetFile.WriteLine($"{item.ProductID}\t{item.CoBoughtProductID}");
                }

                datasetFile.Close();
            }
            var model = CreateHomeIndexViewModel();
            return View("Index", model);
        }

        public IActionResult TrainModels()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var country in countries)
            {
                var mlComtext = new MLContext();
                IDataView dataView = mlComtext.Data.LoadFromTextFile(path: GetPath($"{country}-{datasetName}"),
                columns: new[]
                {
                    new TextLoader.Column(name: "Label", dataKind: DataKind.Double, index: 0),
                    // The key count is the cardinality i.e. maximum
                    // valid value. This column is used internally when
                    // training the model. When results are shown, the
                    // columns are mapped to instances of our model
                    // which could have a different cardinality but
                    // happen to have the same.
                    new TextLoader.Column(name:nameof(ProductCoBought.ProductID), 
                        dataKind: DataKind.UInt32, 
                        source: new[] {new TextLoader.Range(0)}, 
                        keyCount: new KeyCount(77)),
                    new TextLoader.Column(name: nameof(ProductCoBought.CoBoughtProductID), 
                        dataKind: DataKind.UInt32,
                        source: new[]{new TextLoader.Range(1)},
                        keyCount: new KeyCount(77)),
                    
                },hasHeader: true, separatorChar: '\t');

                var options = new MatrixFactorizationTrainer.Options{
                    MatrixColumnIndexColumnName = nameof(ProductCoBought.ProductID),
                    MatrixRowIndexColumnName = nameof(ProductCoBought.CoBoughtProductID),
                    LabelColumnName = "Label",
                    LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass,
                    Alpha = 0.01,
                    Lambda = 0.025,
                    C = 0.00004
                };
                MatrixFactorizationTrainer mft = mlComtext.Recommendation().Trainers.MatrixFactorization(options);
                ITransformer trainedModel = mft.Fit(dataView);

                mlComtext.Model.Save(trainedModel, 
                inputSchema: dataView.Schema,
                filePath: GetPath($"{country}-model.zip"));
            }
            stopwatch.Stop();
            var model = CreateHomeIndexViewModel();
            model.Milliseconds = stopwatch.ElapsedMilliseconds;
            return View("Index",model);
        }

		// GET /Home/Cart
		// To show the cart and recommendations
		// GET /Home/Cart/5
		// To add a product to the cart
		public IActionResult Cart(int? id)
        {
			var cartCookie = Request.Cookies["nw_cart"] ?? string.Empty;
            if (id.HasValue)
            {
                if (String.IsNullOrWhiteSpace(cartCookie))
                {
                    cartCookie = id.ToString();
                }
                else
                {
                    string[] ids = cartCookie.Split('-');
                    if (!ids.Contains(id.ToString()))
                    {
                        cartCookie = String.Join('-',cartCookie, id.ToString());
                    }
                }
                Response.Cookies.Append("nw_cart", cartCookie);
            }
            var model = new HomeCartViewModel
            {
                Cart = new Cart{
                    Items = Enumerable.Empty<CartItem>()
                },
                Reccomendations = new List<EnrinchedReccomendation>()
            };

            if (cartCookie.Length > 0)
            {
                model.Cart.Items = cartCookie.Split('-').Select(item =>
                new CartItem{
                    ProductId = long.Parse(item),
                    ProductName = db.Products.Find(long.Parse(item)).ProductName
                });
            }

            if (System.IO.File.Exists(GetPath("germany-model.zip")))
            {
                var mlComtext = new MLContext();
                ITransformer modelGermany;

                using (var stream= new FileStream(path: GetPath("germany-model.zip"), mode: FileMode.Open, access: FileAccess.Read, share: FileShare.Read))
                {
                    modelGermany = mlComtext.Model.Load(stream, out DataViewSchema schema);
                }

                var predictionEngine = mlComtext.Model.CreatePredictionEngine<ProductCoBought, Reccomendation>(modelGermany);

                var products = db.Products.ToArray();

                foreach (var item in model.Cart.Items)
                {
                    var topThree = products.Select(product => 
                        predictionEngine.Predict(new ProductCoBought{
                                ProductID = (uint)item.ProductId,
                                CoBoughtProductID = (uint)product.ProductID
                        })
                        ).OrderByDescending(x => x.score).Take(3).ToArray();

                    model.Reccomendations.AddRange(topThree
                        .Select(rec => new EnrinchedReccomendation
                        {
                            CoBoughtProductID = rec.CoBoughtProductID,
                            score = rec.score,
                            ProductName = db.Products.Find((long)rec.CoBoughtProductID).ProductName
                        }));
                }

                model.Reccomendations = model.Reccomendations
                    .OrderByDescending(rec => rec.score)
                    .Take(3)
                    .ToList();
            }
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

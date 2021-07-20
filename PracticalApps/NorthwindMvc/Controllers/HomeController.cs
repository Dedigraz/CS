using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using Northwinds;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;

namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Northwind db;
        private readonly IHttpClientFactory clientFactory;
        
        public HomeController(ILogger<HomeController> logger, Northwind injectedContext, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random()).Next(1,10000),
                Categories = await db.Categories.ToListAsync(),
                Products = await db.Products.ToListAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Customers(string country)
        {
            string uri;
            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers Worldwide";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers/?country={country}";
            }

            var client = clientFactory.CreateClient(name: "NorthwindService");

            var request = new HttpRequestMessage(method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage responseMessage = await client.SendAsync(request);

            var model = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<Customer>>();

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async  Task<IActionResult> ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a Product Id in the root for example Home/ProductDetail/43");
            }
            var model = await db.Products.SingleOrDefaultAsync(p =>p.ProductID ==id);

            if (model == null)
            {
                return NotFound($"Product with Product ID of {id} was not found");
            }
            
            return View(model);
        }

        public async Task<IActionResult> CategoryDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("You must pass a Category ID in the root for example Home/CategoryDetail/4");
            }
            var model = await db.Categories.SingleOrDefaultAsync(p => p.CategoryID == id);

            if (model == null)
            {
                return NotFound($"The Category with Category ID of {id} was not found");
            }

            return View(model);
        }

        public IActionResult ModelBinding()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            var model = new HomeModelBindingViewModel
            {
                Thing = thing,
                HasErrors = !ModelState.IsValid,
                ValidationErrors = ModelState.Values
                    .SelectMany(s => s.Errors)
                    .Select(s => s.ErrorMessage)
            };
            return View(model);
        }

        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {
            if (!price.HasValue)
            {
                return NotFound("You must pass a value into the query for example, home/ProductThatCostMoreThan?price=50");
            }

            IEnumerable<Product> model = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.UnitPrice > price);
            if (model.Count() == 0)
            {
                return NotFound("There's no product above this price");
            }

            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model);
        }
    }
}

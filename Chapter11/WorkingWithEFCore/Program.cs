using System;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace WorkingWithEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // QueryingWithLike();
            QueryingCategories();
            // QueryingProducts();
        }
        static void QueryingCategories()
        {
            using (var db = new Northwind())
            {
                // var loggerFactory = db.GetService<ILoggerFactory>();
                // loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Categories and how many products they have:");

                IQueryable<Category> cats;//db.Categories.Include(c => c.Products);

                db.ChangeTracker.LazyLoadingEnabled = false;


                Write("Enable eager loading: Y/N");
                bool eagerloading = ReadKey().Key == ConsoleKey.Y;
                bool explicitloading = false;

                if(eagerloading == true)
                {
                    cats = db.Categories.Include(c => c.Products);
                }
                else
                {
                    cats = db.Categories;
                    Write("Enable explicit loading? (Y/N): ");
                    explicitloading = (ReadKey().Key == ConsoleKey.Y);
                    WriteLine();
                }
                foreach (Category c in cats)
                {
                    if (explicitloading)
                    {
                        
                    }
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products");
                }

            }
        }
        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Products that cost more than a price, highest atop.");
                string input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));
                // Write("Enter a product price: ");
                // input = ReadLine();
                // decimal price = decimal.Parse(input);

                IOrderedEnumerable<Product> prods = db.Products
                    .AsEnumerable()
                    .Where(predicate: product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);

                
                foreach (Product item in prods)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",item.ProductID, item.ProductName, item.Cost, item.Stock);
                }
            }
        }

        static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                Write("Write the name of the product: ");
                string input = ReadLine();

                IQueryable<Product>products = db.Products.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));

                foreach (var item in products)
                {
                    WriteLine("{0} has {1} units in stock. Discontinued? {2}",item.ProductName, item.Stock, item.Discontinued);
                }
            }
        }

    }
}

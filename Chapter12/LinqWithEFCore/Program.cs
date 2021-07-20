using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Sqlite;
using static System.Console;

namespace LinqWithEFCore
{
    class Program
    {
        static void SortAndFilter()
        {
            using (var db = new Northwind())
            {
                var query = db.Products
                    .ProcessSequence()
                    .Where(product => product.UnitPrice < 10)
                    .OrderByDescending(product => product.UnitPrice);

                WriteLine("Products that cost less than $10");
                foreach (var item in query)
                {
                    WriteLine($"{item.ProductID}: {item.ProductName} costs {item.UnitPrice:$#,##0.00}price");
                    WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            SortAndFilter();
        }

    }
}

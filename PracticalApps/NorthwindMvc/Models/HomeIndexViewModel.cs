using System.Collections.Generic;
using Northwinds;
namespace NorthwindMvc.Models
{
    public class HomeIndexViewModel
    {
        public int VisitorCount{get; set;}
        public IList<Product> Products{get; set;}
        public IList<Category> Categories{get; set;}
    }
}
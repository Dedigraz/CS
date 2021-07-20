using Northwinds;
using System.Collections.Generic;
namespace NorthwindMvc.Models
{
    public class HomeCategoryDetailViewModel
    {
        public Category Category{get; set;}
        public ICollection<Product> Products{get; set;}
    }
}
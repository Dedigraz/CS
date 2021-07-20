using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Northwinds;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindWeb.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<string> Suppliers{get; set;}
        public Northwind db;

        public SuppliersModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public void OnGet()
        {
            ViewData["Title"] = "Northwind Web Site - Suppliers";

            Suppliers = db.Suppliers.Select(p => p.CompanyName);
        }

		[BindProperty]
        public Supplier Supplier{get; set;}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				db.Suppliers.Add(Supplier);
				db.SaveChanges();

				return RedirectToPage("suppliers");
			}
			return Page();
		}
    }
}
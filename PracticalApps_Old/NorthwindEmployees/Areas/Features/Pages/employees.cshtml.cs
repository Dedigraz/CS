using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwinds;
using System.Linq;
using System.Collections.Generic;

namespace features.Pages
{
    public class EmployeesPageModel : PageModel
    {
        private Northwind db;

        public EmployeesPageModel(Northwind injectedContext)
        {
            db = injectedContext;
        }

        public IEnumerable<Employee> Employees{get; set;}

        public void OnGet()
        {
            Employees = db.Employees.ToArray();
        }
    }
}
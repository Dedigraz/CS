using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Northwinds;
namespace NorthwindBlazorServer.Data
{
    public class NorthwindService : INorthwindService
    {
        private readonly Northwind db;
        public NorthwindService(Northwind db)
        {
            this.db = db;   
        }

		Task<Customer> INorthwindService.CreateCustomerAsync(Customer c)
		{
			db.Customers.Add(c);
            db.SaveChangesAsync();
            return Task.FromResult<Customer> (c);
		}

		Task INorthwindService.DeleteCustomerAsync(string id)
		{
			Customer customer = db.Customers.FirstOrDefaultAsync(c => c.CustomerID == id).Result;
			db.Customers.Remove(customer);
			return db.SaveChangesAsync();
		}

		Task<Customer> INorthwindService.GetCustomerAsync(string id)
		{
			return db.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
		}

		Task<List<Customer>> INorthwindService.GetCustomersAsync()
		{
			return db.Customers.ToListAsync();
		}

		Task<Customer> INorthwindService.UpdateCustomerAsync(Customer c)
		{
			db.Entry(c).State = EntityState.Modified;
			db.SaveChangesAsync();
			return Task.FromResult<Customer> (c);
		}
	}
}
using System;
using System.Threading.Tasks;
using NorthwindMobile.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace NorthwindMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomersList : ContentPage
	{
		public CustomersList()
		{
			InitializeComponent();

			Customer.Customers.Clear();
			try
			{
				var client = new HttpClient
				{
					BaseAddress = new Uri("http://localhost:5003/")
				};
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = client.GetAsync("api/customers").Result;


				response.EnsureSuccessStatusCode();

				string content = response.Content.ReadAsStringAsync().Result;

				var customersFromService = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);

				foreach (Customer c in customersFromService.OrderBy(customer => customer.CompanyName))
				{
					Customer.Customers.Add(c);
				}
			}
			catch (Exception ex)
			{
				DisplayAlert(title: "Exception",
				message: $"App will use sample data due to: {ex.Message}",
				cancel: "OK");
				Customer.AddSampleData();
			}
			BindingContext = Customer.Customers;
		}

		private async  void Customers_Tapped(object sender, ItemTappedEventArgs e)
		{
			var c = e.Item as Customer;

			if (c == null) return;

			await Navigation.PushAsync(new CustomerDetails(c));
		}

		private async void Customers_Refreshing(object sender, EventArgs e)
		{
			var listView = sender as ListView;

			listView.IsRefreshing = true;

			//simulate a refresh
			await Task.Delay(1500);
			listView.IsRefreshing = false;
		}

		private async void Customer_Phoned(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			Customer c = menuItem.BindingContext as Customer;

			if (await this.DisplayAlert("Dial a number", "Would you like to call "+ c.Phone+ "?", "Yes", "No"))
			{
				var dialer = DependencyService.Get<IDialer>();

				if (dialer != null) dialer.Dial(c.Phone);
			}
		}


		private void Customer_Deleted(object sender, EventArgs e)
		{
			var menuItem = sender as MenuItem;
			Customer c = menuItem.BindingContext as Customer;
			Customer.Customers.Remove(c);
		}

		private async void Add_Activated(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CustomerDetails());
		}
	}
}
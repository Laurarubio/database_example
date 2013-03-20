using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SQLiteDemo.ViewModels
{
    public class CustomersViewModel : ViewModelBase
    {
        private ObservableCollection<CustomerViewModel> customers;
        public ObservableCollection<CustomerViewModel> Customers
        {
            get
            {
                return customers;
            }

            set
            {
                customers = value;
                RaisePropertyChanged("Customers");
            }
        }
        private SQLiteDemo.App app = (Application.Current as App);

        public ObservableCollection<CustomerViewModel> GetCustomers()
        {
            customers = new ObservableCollection<CustomerViewModel>();
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var query = db.Table<Customer>().OrderBy(c => c.Name);
                foreach (var _customer in query)
                {
                    var customer = new CustomerViewModel()
                    {
                        Id = _customer.Id,
                        Name = _customer.Name,
                        City = _customer.City,
                        Contact = _customer.Contact
                    };
                    customers.Add(customer);
                }
            }
            return customers;
        }

    }
}

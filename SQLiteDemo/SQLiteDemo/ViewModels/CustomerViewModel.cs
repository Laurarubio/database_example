using SQLiteDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SQLiteDemo.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        #region Properties

        private int id = 0;
        public int Id
        {
            get
            { return id; }

            set
            {
                if (id == value)
                { return; }

                id = value;
                RaisePropertyChanged("Id");
            }
        }

        private string name = string.Empty;
        public string Name
        {
            get
            { return name; }

            set
            {
                if (name == value)
                { return; }

                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string city = string.Empty;
        public string City
        {
            get
            { return city; }

            set
            {
                if (city == value)
                { return; }

                city = value;
                RaisePropertyChanged("City");
            }
        }

        private string contact = string.Empty;
        public string Contact
        {
            get
            { return contact; }

            set
            {
                if (contact == value)
                { return; }

                contact = value;
                RaisePropertyChanged("Contact");
            }
        }

        #endregion "Properties"

        private SQLiteDemo.App app = (Application.Current as App);

        public CustomerViewModel GetCustomer(int customerId)
        {
            var customer = new CustomerViewModel();
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var _customer = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();
                customer.Id = _customer.Id;
                customer.Name = _customer.Name;
                customer.City = _customer.City;
                customer.Contact = _customer.Contact;
            }
            return customer;
        }

        public string GetCustomerName(int customerId)
        {
            string customerName = "Unknown";
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var customer = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();
                customerName = customer.Name;
            }
            return customerName;
        }

        public string SaveCustomer(CustomerViewModel customer)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                string change = string.Empty;
                try
                {
                    var existingCustomer = (db.Table<Customer>().Where(
                        c => c.Id == customer.Id)).SingleOrDefault();

                    if (existingCustomer != null)
                    {
                        existingCustomer.Name = customer.Name;
                        existingCustomer.City = customer.City;
                        existingCustomer.Contact = customer.Contact;
                        int success = db.Update(existingCustomer);
                    }
                    else
                    {
                        int success = db.Insert(new Customer()
                        {
                            Id = customer.id,
                            Name = customer.Name,
                            City = customer.City,
                            Contact = customer.Contact
                        });
                    }
                    result = "Success";
                }
                catch (Exception ex)
                {
                    result = "This customer was not saved.";
                }
            }
            return result;
        }

        public string DeleteCustomer(int customerId)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var projects = db.Table<Project>().Where(
                    p => p.CustomerId == customerId);
                foreach (Project project in projects)
                {
                    db.Delete(project);
                }
                var existingCustomer = (db.Table<Customer>().Where(
                    c => c.Id == customerId)).Single();

                if (db.Delete(existingCustomer) > 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "This customer was not removed";
                }
            }
            return result;
        }

        public int GetNewCustomerId()
        {
            int customerId = 0;
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var customers = db.Table<Customer>();
                if (customers.Count() > 0)
                {
                    customerId = (from c in db.Table<Customer>()
                                  select c.Id).Max();
                    customerId += 1;
                }
                else
                {
                    customerId = 1;
                }
            }
            return customerId;
        }

    }
}

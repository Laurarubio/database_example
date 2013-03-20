using SQLiteDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SQLiteDemo.Views
{
    public sealed partial class CustomerPage : SQLiteDemo.Common.LayoutAwarePage
    {
        CustomerViewModel customer = null;
        private SQLiteDemo.App app = (Application.Current as App);

        public CustomerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                customer = new CustomerViewModel();
                customer.Id = customer.GetNewCustomerId();
                PageTitle.Text = "New customer";
            }
            else
            {
                customer = (CustomerViewModel)e.Parameter;
                PageTitle.Text = customer.Name;
            }
            this.DataContext = customer;
            app.CurrentCustomerId = customer.Id;

            base.OnNavigatedTo(e);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string result = customer.SaveCustomer(customer);
            if (result.Contains("Success"))
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string result = customer.DeleteCustomer(customer.Id);
            if (result.Contains("Success"))
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

    }
}

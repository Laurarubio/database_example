using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLiteDemo.Models;
using SQLiteDemo.Views;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
//public string DBPath { get; set; }
//public int CurrentCustomerId { get; set; }

namespace SQLiteDemo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public string DBPath { get; set; }
        public int CurrentCustomerId { get; set; }
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();

            // Get a reference to the SQLite database
            this.DBPath = Path.Combine(
                Windows.Storage.ApplicationData.Current.LocalFolder.Path, "customers.sqlite");
            // Initialize the database if necessary
            using (var db = new SQLite.SQLiteConnection(this.DBPath))
            {
                // Create the tables if they don't exist
                db.CreateTable<Customer>();
                db.CreateTable<Project>();
            }
            // Place the frame in the current Window
            Window.Current.Content = rootFrame;

            using (var db = new SQLite.SQLiteConnection(this.DBPath))
            {
                // Create the tables if they don't exist
                db.CreateTable<Customer>();
                db.CreateTable<Project>();
            }
            ResetData();

        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void ResetData()
        {
            using (var db = new SQLite.SQLiteConnection(this.DBPath))
            {
                // Empty the Customer and Project tables 
                db.DeleteAll<Customer>();
                db.DeleteAll<Project>();

                // Add seed customers and projects
                db.Insert(new Customer()
                {
                    Id = 1,
                    Name = "Adventure Works",
                    City = "Bellevue",
                    Contact = "Mu Han"
                });
                db.Insert(new Customer()
                {
                    Id = 2,
                    Name = "Contoso",
                    City = "Seattle",
                    Contact = "David Hamilton"
                });
                db.Insert(new Customer()
                {
                    Id = 3,
                    Name = "Fabrikam",
                    City = "Redmond",
                    Contact = "Guido Pica"
                });
                db.Insert(new Customer()
                {
                    Id = 4,
                    Name = "Tailspin Toys",
                    City = "Kent",
                    Contact = "Michelle Alexander"
                });

                db.Insert(new Project()
                {
                    Id = 1,
                    CustomerId = 1,
                    Name = "Windows Store app",
                    Description = "Expense reports",
                    DueDate = DateTime.Today.AddDays(4)
                });
                db.Insert(new Project()
                {
                    Id = 2,
                    CustomerId = 1,
                    Name = "Windows Store app",
                    Description = "Time reporting",
                    DueDate = DateTime.Today.AddDays(14)
                });
                db.Insert(new Project()
                {
                    Id = 3,
                    CustomerId = 1,
                    Name = "Windows Store app",
                    Description = "Project management",
                    DueDate = DateTime.Today.AddDays(24)
                });
                db.Insert(new Project()
                {
                    Id = 4,
                    CustomerId = 2,
                    Name = "Windows Phone app",
                    Description = "Soccer scheduling",
                    DueDate = DateTime.Today.AddDays(6)
                });
                db.Insert(new Project()
                {
                    Id = 5,
                    CustomerId = 3,
                    Name = "MVC4 app",
                    Description = "Catalog",
                    DueDate = DateTime.Today.AddDays(30)
                });
                db.Insert(new Project()
                {
                    Id = 6,
                    CustomerId = 3,
                    Name = "MVC4 app",
                    Description = "Expense reports",
                    DueDate = DateTime.Today.AddDays(-3)
                });
                db.Insert(new Project()
                {
                    Id = 7,
                    CustomerId = 3,
                    Name = "Windows Store app",
                    Description = "Expense reports",
                    DueDate = DateTime.Today.AddDays(45)
                });
                db.Insert(new Project()
                {
                    Id = 8,
                    CustomerId = 4,
                    Name = "Windows Store app",
                    Description = "Kids game",
                    DueDate = DateTime.Today.AddDays(60)
                });

            }
        }
    }
}

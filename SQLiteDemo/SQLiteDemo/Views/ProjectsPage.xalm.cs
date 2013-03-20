using SQLiteDemo.Models;
using SQLiteDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ProjectsPage : SQLiteDemo.Common.LayoutAwarePage
    {
        ProjectsViewModel projectsViewModel = null;
        ObservableCollection<ProjectViewModel> projects = null;
        private SQLiteDemo.App app = (Application.Current as App);

        public ProjectsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var customer = (CustomerViewModel)e.Parameter;
            app.CurrentCustomerId = customer.Id;
            projectsViewModel = new ProjectsViewModel();
            projects = projectsViewModel.GetProjects(customer.Id);
            ProjectsViewSource.Source = projects;
            ProjectsGridView.SelectedItem = null;
            PageTitle.Text = string.Format("{0} projects", customer.Name);

            base.OnNavigatedTo(e);
        }

        private void ProjectsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ProjectPage), e.ClickedItem);
        }

        private void ProjectsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectsGridView.SelectedItems.Count() > 0)
            {
                ProjectsPageAppBar.IsOpen = true;
                ProjectsPageAppBar.IsSticky = true;
                AddButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Visible;
            }
            else
            {
                ProjectsPageAppBar.IsOpen = false;
                ProjectsPageAppBar.IsSticky = false;
                AddButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProjectPage));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProjectPage), ProjectsGridView.SelectedItem);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Taggy.Model;
using Taggy.ViewModel;

namespace Taggy.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Loaded += OnLoaded;
            Closing += OnClosing;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
                viewModel.Load();
        }

        private void OnClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
                viewModel.Save();
        }

        public void Browse()
        {
            // create folder browser dialog
        }

		private void AddResourcesButtonClick(object sender, RoutedEventArgs e)
		{
            var dialog = new ResourceAdditionDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                var result = dialog.Result;
                var resources = new List<Resource>();
                resources.Add(result);
                var mainViewModel = DataContext as MainViewModel;
                if (mainViewModel != null)
                    mainViewModel.AddResources(resources);
            }
		}

        private void EditResourcesButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedResources = SelectedResources;

            var location = string.Empty;
            var locations = selectedResources.Select(r => r.Location).Distinct().ToList();
            if (locations.Count == 1)
                location = locations.First();

            var tags = new Tags();
            var tagsCollections = selectedResources.Select(r => r.Tags).ToList();
            if (tagsCollections.Count == 1)
                tags = tagsCollections.First();

            var dialog = new ResourceAdditionDialog();
            dialog.Owner = this;
            dialog.Location = location;
            dialog.Tags = tags;
            if (dialog.ShowDialog() == true)
            {
                var mainViewModel = DataContext as MainViewModel;
                if (mainViewModel != null)
                    mainViewModel.EditResources(selectedResources, dialog.Result);
            }
        }

		private void RemoveResourcesButtonClick(object sender, RoutedEventArgs e)
		{
            var mainViewModel = DataContext as MainViewModel;
            if (mainViewModel != null)
                mainViewModel.RemoveResources(SelectedResources);
		}

        private IEnumerable<Resource> SelectedResources
        {
            get
            {
                var selectedItems = ResourcesDataGrid.SelectedItems;
                var selectedValues = selectedItems.OfType<Resource>();
                return selectedValues;
            }
        }
    }
}

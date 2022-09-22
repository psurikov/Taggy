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

        public void Reindex()
        {
            if (DataContext is MainViewModel viewModel)
                viewModel.Reindex();
        }

        private void OnReindex(object sender, RoutedEventArgs e)
        {
            Reindex();
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
                if (DataContext is MainViewModel viewModel)
                    viewModel.AddResources(resources);
            }
		}

        private void EditResourcesButtonClick(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            if (mainViewModel != null)
            {
                var selectedResources = mainViewModel.Resources;
                var locations = selectedResources.Select(r => r.Location);
                var tags = selectedResources.Select(r => r.Tags);
                var dialog = new ResourceAdditionDialog();
                dialog.Owner = this;
                dialog.Location = locations.FirstOrDefault() ?? string.Empty;
                if (dialog.ShowDialog() == true)
                {

                }
            }
        }

		private void RemoveResourcesButtonClick(object sender, RoutedEventArgs e)
		{
            if (DataContext is MainViewModel viewModel)
                viewModel.RemoveResources(SelectedResources);
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

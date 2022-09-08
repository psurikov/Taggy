using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            var dialog = new ResourcesAdditionDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                if (DataContext is MainViewModel viewModel)
                    viewModel.AddResources();
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

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
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null)
                viewModel.Load();
        }

        private void OnClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null)
                viewModel.Save();
        }

        public void Browse()
        {
            // create folder browser dialog
        }

        public void Reindex()
        {
            var dataContext = DataContext as MainViewModel;
            if (dataContext != null)
                dataContext.Reindex();
        }

        private void OnReindex(object sender, RoutedEventArgs e)
        {
            Reindex();
        }
    }
}

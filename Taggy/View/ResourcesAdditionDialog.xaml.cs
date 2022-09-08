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
using System.Windows.Shapes;
using Taggy.ViewModel;

namespace Taggy.View
{
	/// <summary>
	/// Interaction logic for ResourcesAdditionDialog.xaml
	/// </summary>
	public partial class ResourcesAdditionDialog : Window
	{
		public ResourcesAdditionDialog()
		{
			InitializeComponent();
			var tagCloud = new TagCloudViewModel();
			TagsCloud.DataContext = tagCloud;
			tagCloud.Items = new System.Collections.ObjectModel.ObservableCollection<TagCloudItemViewModel>();
			tagCloud.Items.Add(new TagCloudItemViewModel() { Tag = new Model.Tag("Physics") });
			tagCloud.Items.Add(new TagCloudItemViewModel() { Tag = new Model.Tag("Math") });
			tagCloud.Items.Add(new TagCloudItemViewModel() { Tag = new Model.Tag("Networking") });
		}

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
			DialogResult = true;
			Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
			//DialogResult = false;
			Close();
        }
    }
}

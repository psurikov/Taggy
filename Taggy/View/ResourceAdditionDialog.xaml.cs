using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Taggy.Model;
using Taggy.ViewModel;

namespace Taggy.View
{
    public partial class ResourceAdditionDialog : Window, INotifyPropertyChanged
    {
        private string location;
        private string tagsString;
        private TagSelectionViewModel tagSelection;

        public ResourceAdditionDialog()
        {
            InitializeComponent();
            var tagSelection = new TagSelectionViewModel();
            TagSelectionControl.DataContext = tagSelection;
            tagSelection.Items = new System.Collections.ObjectModel.ObservableCollection<TagSelectionElementViewModel>();
            tagSelection.Items.Add(new TagSelectionElementViewModel() { Tag = new Tag("Physics") });
            tagSelection.Items.Add(new TagSelectionElementViewModel() { Tag = new Tag("Math") });
            tagSelection.Items.Add(new TagSelectionElementViewModel() { Tag = new Tag("Networking") });
        }

        #region Properties

        public string Location
        {
            get { return location; }
            set
            {
                if (location == value)
                    return;
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public string TagsString
        {
            get { return tagsString; }
            set
            {
                if (tagsString == value)
                    return;
                tagsString = value;
                OnPropertyChanged(nameof(TagsString));
            }
        }

        public TagSelectionViewModel TagSelection
        {
            get { return tagSelection; }
            set
            {
                if (tagSelection == value)
                    return;
                tagSelection = value;
                OnPropertyChanged(nameof(TagSelection));
            }
        }

        public Resource Result
        {
            get
            {
                var location = Location;
                var tags = TagsConverter.FromString(TagsString);
                var resource = new Resource(location, tags, DateTime.Now);
                return resource;
            }
        }

        #endregion

        #region Handlers

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

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}

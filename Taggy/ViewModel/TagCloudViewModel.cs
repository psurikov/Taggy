using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Taggy.ViewModel
{
    public class TagCloudViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TagCloudItemViewModel> items;

        #region Constructors

        public TagCloudViewModel()
        {
            items = new ObservableCollection<TagCloudItemViewModel>();
        }

        #endregion

        #region Properties

        public ObservableCollection<TagCloudItemViewModel> Items
        {
            get { return items; }
            set
            {
                if (items == value)
                    return;
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        #endregion

        #region Methods

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}

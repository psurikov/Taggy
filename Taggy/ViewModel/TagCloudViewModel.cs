using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Taggy.ViewModel
{
    public class TagCloudViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TagCloudItemViewModel> items;
        private ObservableCollection<TagCloudItemViewModel> selectedItems;

        #region Constructors

        public TagCloudViewModel()
        {
            items = new ObservableCollection<TagCloudItemViewModel>();
            selectedItems = new ObservableCollection<TagCloudItemViewModel>();
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

        public ObservableCollection<TagCloudItemViewModel> SelectedItems
        {
            get { return selectedItems; }
            set
            {
                if (selectedItems == value)
                    return;
                UnsubscribeSelectedItemsChanged();
                selectedItems = value;
                SubscribeSelectedItemsChanged();
                OnPropertyChanged(nameof(SelectedItems));
                UpdateSelectedItems();
            }
        }

        #endregion

        #region Methods

        private void SubscribeSelectedItemsChanged()
        {
            selectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
            selectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        private void UnsubscribeSelectedItemsChanged()
        {
            selectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
        }

        private void SelectedItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedItems();
        }

        private void UpdateSelectedItems()
        {
            if (items == null)
                return;
            if (selectedItems == null)
                return;
            var unselectedItems = items.Except(selectedItems);
            foreach (var unselectedItem in unselectedItems)
                unselectedItem.IsSelected = false;
            foreach (var selectedItem in selectedItems)
                selectedItem.IsSelected = true;
        }

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

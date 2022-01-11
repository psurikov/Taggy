using System.ComponentModel;
using Taggy.Model;

namespace Taggy.ViewModel
{
    public class TagCloudItemViewModel : INotifyPropertyChanged
    {
        private Tag tag;
        private int weight;

        #region Constructors

        public TagCloudItemViewModel()
        {
            Tag = new Tag();
        }

        #endregion

        #region Properties

        public Tag Tag
        {
            get { return tag; }
            set
            {
                if (tag == value)
                    return;
                tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }

        public int Weight
        {
            get { return weight; }
            set
            {
                if (weight == value)
                    return;
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
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

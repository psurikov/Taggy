using System.ComponentModel;

namespace Taggy.Model
{
    public class Resource : INotifyPropertyChanged
    {
        private string location;
        private Tags tags;

        public Resource(string location, Tags tags)
        {
            this.location = location;
            this.tags = tags;
        }

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

        public Tags Tags
        {
            get { return tags; }
            set
            {
                if (tags == value)
                    return;
                tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

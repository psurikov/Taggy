using System;
using System.ComponentModel;

namespace Taggy.Model
{
    public class Resource : INotifyPropertyChanged
    {
        private string location;
        private Tags tags;
        private DateTime dateAdded;

        public Resource(string location, Tags tags, DateTime dateAdded)
        {
            this.location = location;
            this.tags = tags;
            this.dateAdded = dateAdded;
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

        public DateTime DateAdded
        {
            get { return dateAdded; }
            set
            {
                if (dateAdded.Equals(value))
                    return;
                dateAdded = value;
                OnPropertyChanged(nameof(DateAdded));
            }
        }

        public string TagsString
        {
            get { return tags.ToString(); }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

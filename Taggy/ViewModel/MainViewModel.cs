using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taggy.Model;

namespace Taggy.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string location;
        private ObservableCollection<Tag> tags = new ObservableCollection<Tag>();
        private Tag selectedTag;
        private ObservableCollection<FileReference> fileReferences = new ObservableCollection<FileReference>();        
        private ObservableCollection<FileReference> fileReferencesForSelectedTag = new ObservableCollection<FileReference>();

        #endregion

        #region Constructors

        public MainViewModel()
        {
            Location = "S:\\Books";
        }

        #endregion

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

        public ObservableCollection<Tag> Tags
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

        public Tag SelectedTag
        {
            get { return selectedTag; }
            set
            {
                if (selectedTag == value)
                    return;
                selectedTag = value;
                OnPropertyChanged(nameof(SelectedTag));
            }
        }

        public ObservableCollection<FileReference> FileReferences
        {
            get { return fileReferences; }
            set
            {
                if (fileReferences == value)
                    return;
                fileReferences = value;
                OnPropertyChanged(nameof(FileReferences));
                UpdateFileReferencesForSelectedTag();
            }
        }

        public ObservableCollection<FileReference> FileReferencesForSelectedTag
        {
            get { return fileReferencesForSelectedTag; }
            set
            {
                if (fileReferencesForSelectedTag == value)
                    return;
                fileReferencesForSelectedTag = value;
                OnPropertyChanged(nameof(FileReferencesForSelectedTag));
            }
        }

        #endregion

        #region Actions

        public void Reindex()
        {
            var fileReferences = FileReferenceBrowser.Browse(Location);
            this.FileReferences = new ObservableCollection<FileReference>(fileReferences);

            var concatenatedTags = fileReferences.SelectMany(f => f.TagCluster.Items);
            var distinctTags = concatenatedTags.Distinct().OrderBy(t => t.Name + "#" + t.Value);
            Tags = new ObservableCollection<Tag>(distinctTags);
        }

        #endregion

        #region Other Methods

        private void UpdateFileReferencesForSelectedTag()
        {
            FileReferencesForSelectedTag = new ObservableCollection<FileReference>(FileReferences);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

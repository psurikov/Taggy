using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Taggy.Model
{
    /// <summary>
    /// Tag cloud.
    /// </summary>
    public class TagCloud : INotifyPropertyChanged
    {
        private ObservableCollection<Resource> resources = new();
        private ObservableCollection<Resource> filteredResources = new();
        private ObservableCollection<Tag> tags = new();
        private ObservableCollection<Tag> selectedTags = new();
        private ObservableCollection<Tag> filteredTags = new();
        //private ObservableCollection<TagFrequency> filteredTagFrequencies = new();
        private ObservableCollection<string> categories = new();
        private ObservableCollection<string> selectedCategories = new();
        private string resourcesFilter = string.Empty;
        private string tagFilter = string.Empty;
        private readonly Dictionary<Tag, int> tagsOccurrences = new();

        public TagCloud() { }
        public TagCloud(ObservableCollection<Resource> resources)
        {
            Resources = resources;
        }

        #region Properties

        public ObservableCollection<Resource> Resources
        {
            get { return resources; }
            set
            {
                if (resources == value)
                    return;
                resources.CollectionChanged -= OnResourcesChanged;
                resources = value;
                resources.CollectionChanged -= OnResourcesChanged;
                resources.CollectionChanged += OnResourcesChanged;
                OnResourcesChanged();
                OnPropertyChanged(nameof(Resources));

            }
        }

        public ObservableCollection<Resource> FilteredResources
        {
            get { return filteredResources; }
            set
            {
                if (filteredResources == value)
                    return;
                filteredResources.CollectionChanged -= OnFilteredResourcesChanged;
                filteredResources = value;
                filteredResources.CollectionChanged -= OnFilteredResourcesChanged;
                filteredResources.CollectionChanged += OnFilteredResourcesChanged;
                OnPropertyChanged(nameof(FilteredResources));
            }
        }

        public ObservableCollection<Tag> Tags
        {
            get { return tags; }
            set
            {
                if (tags == value)
                    return;
                tags.CollectionChanged -= OnTagsChanged;
                tags = value;
                tags.CollectionChanged -= OnTagsChanged;
                tags.CollectionChanged += OnTagsChanged;
                OnTagsChanged();
                OnPropertyChanged(nameof(Tags));
            }
        }

        public ObservableCollection<Tag> SelectedTags
        {
            get { return selectedTags; }
            set
            {
                if (selectedTags == value)
                    return;
                selectedTags.CollectionChanged -= OnSelectedTagsChanged;
                selectedTags = value;
                selectedTags.CollectionChanged -= OnSelectedTagsChanged;
                selectedTags.CollectionChanged += OnSelectedTagsChanged;
                OnSelectedTagsChanged();
                OnPropertyChanged(nameof(SelectedTags));
            }
        }

        public ObservableCollection<Tag> FilteredTags
        {
            get { return filteredTags; }
            set
            {
                if (filteredTags == value)
                    return;
                filteredTags = value;
                OnPropertyChanged(nameof(FilteredTags));
                //UpdateFilteredTagFrequencies();
            }
        }

        /*public ObservableCollection<TagFrequency> FilteredTagFrequencies
        {
            get { return filteredTagFrequencies; }
            set
            {
                if (filteredTagFrequencies == value)
                    return;
                filteredTagFrequencies = value;
                OnPropertyChanged(nameof(FilteredTagFrequencies));
            }
        }*/

        public ObservableCollection<string> Categories
        {
            get { return categories; }
            set
            {
                if (categories == value)
                    return;
                categories = value;
                OnPropertyChanged(nameof(Categories));
                UpdateSelectedCategories();
            }
        }

        public ObservableCollection<string> SelectedCategories
        {
            get { return selectedCategories; }
            set
            {
                if (selectedCategories == value)
                    return;
                selectedCategories = value;
                OnPropertyChanged(nameof(SelectedCategories));
            }
        }

        public string ResourcesFilter
        {
            get { return resourcesFilter; }
            set
            {
                if (resourcesFilter == value)
                    return;
                resourcesFilter = value;
                OnPropertyChanged(nameof(ResourcesFilter));
                UpdateFilteredResources();
            }
        }

        public string TagsFilter
        {
            get { return tagFilter; }
            set
            {
                if (tagFilter == value)
                    return;
                tagFilter = value;
                OnPropertyChanged(nameof(TagsFilter));
            }
        }

        public Dictionary<Tag, int> TagOccurrences
        {
            get { return tagsOccurrences; }
        }

        #endregion

        #region Methods

        private void UpdateTags()
        {
            tagsOccurrences.Clear();
            var tags = resources.SelectMany(r => r.Tags.Items);
            foreach (var tag in tags)
            {
                if (!tagsOccurrences.ContainsKey(tag))
                    tagsOccurrences[tag] = 0;
                tagsOccurrences[tag]++;
            }

            Tags = new ObservableCollection<Tag>(tagsOccurrences.Keys);
        }

        private void UpdateFilteredTags()
        {
            var newTags = new ObservableCollection<Tag>();
            var tags = filteredResources.SelectMany(r => r.Tags.Items).ToHashSet();
            foreach (var tag in tags)
            {
                var nameMatch = tag.Name.Contains(tagFilter);
                if (nameMatch == false)
                    continue;
                var categoryMatch = (selectedCategories.Count == 0) || selectedCategories.Contains(tag.Category);
                if (categoryMatch == false)
                    continue;
                var matchByTagFilter = string.IsNullOrWhiteSpace(tagFilter) || tag.ToString().Contains(tagFilter);
                if (matchByTagFilter == false)
                    continue;
                newTags.Add(tag);
            }
            if (!CompareTags(FilteredTags, newTags))
                FilteredTags = newTags;
        }

        /*private void UpdateFilteredTagFrequencies()
        {
            var newFilteredTagFrequencies = new ObservableCollection<TagFrequency>();
            foreach (var filteredTag in filteredTags)
            {
                var filteredTagFrequency = new TagFrequency();
                filteredTagFrequency.Tag = filteredTag;
                filteredTagFrequency.Frequency = tagsOccurrences[filteredTag] / (double)tagsOccurrences.Count;
                filteredTagFrequency.Occurrences = tagsOccurrences[filteredTag];
                newFilteredTagFrequencies.Add(filteredTagFrequency);
            }

            FilteredTagFrequencies = newFilteredTagFrequencies;
        }*/

        private void UpdateFilteredResources()
        {
            var newResources = new ObservableCollection<Resource>();
            foreach (var resource in resources)
            {
                var tags = resource.Tags.Items;
                var matchBySelectedTags = (selectedTags.Count == 0) || selectedTags.Intersect(tags).Any();
                if (matchBySelectedTags == false)
                    continue;
                var matchByResourceFilter = string.IsNullOrWhiteSpace(resourcesFilter) ||
                    resource.Location.Contains(resourcesFilter) ||
                    resource.TagsString.Contains(resourcesFilter);
                if (matchByResourceFilter == false)
                    continue;
                newResources.Add(resource);
            }

            if (!CompareResources(FilteredResources, newResources))
                FilteredResources = newResources;
        }

        private void UpdateCategories()
        {
            var categories = new SortedSet<string>();
            foreach (var tag in tags)
                categories.Add(tag.Category);
            Categories = new ObservableCollection<string>(categories);
        }

        private void UpdateSelectedCategories()
        {
            foreach (var category in categories)
            {
                if (!selectedCategories.Contains(category))
                    selectedCategories.Remove(category);
            }
        }

        private static bool CompareResources(ObservableCollection<Resource> collection1, ObservableCollection<Resource> collection2)
        {
            if (collection1 == null && collection2 == null)
                return true;
            if (collection1 == null || collection2 == null)
                return false;
            if (collection1.Count != collection2.Count)
                return false;
            for (var i = 0; i < collection1.Count; i++)
            {
                var resource1 = collection1[i];
                var resource2 = collection2[i];
                if (resource1.Location != resource2.Location)
                    return false;
                if (resource1.DateAdded != resource2.DateAdded)
                    return false;
                if (resource1.Tags.ToString() != resource2.Tags.ToString())
                    return false;
            }

            return true;
        }

        private static bool CompareTags(ObservableCollection<Tag> tags1, ObservableCollection<Tag> tags2)
        {
            if (tags1 == null && tags2 == null)
                return true;
            if (tags1 == null || tags2 == null)
                return false;
            if (tags1.Count != tags2.Count)
                return false;
            for (var i = 0; i < tags1.Count; i++)
            {
                var tag1 = tags1[i];
                var tag2 = tags2[i];
                if (tag1.ToString() != tag2.ToString())
                    return false;
            }

            return true;
        }

        private void OnTagsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnTagsChanged();
        }

        private void OnTagsChanged()
        {
            UpdateCategories();
        }

        private void OnResourcesChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            OnResourcesChanged();
        }

        private void OnResourcesChanged()
        {
            UpdateTags();
        }

        private void OnFilteredResourcesChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            OnFilteredResourcesChanged();
        }

        private void OnFilteredResourcesChanged()
        {
            UpdateFilteredTags();
        }

        private void OnSelectedTagsChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            OnSelectedTagsChanged();
        }

        private void OnSelectedTagsChanged()
        {
            UpdateFilteredResources();
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

    /*public class TagFrequency
    {
        private Tag tag;
        private double frequency;
        private int occurrences;

        #region Constructors

        public TagFrequency()
        {
            tag = new Tag();
            frequency = 0;
            occurrences = 0;
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

        public double Frequency
        {
            get { return frequency; }
            set
            {
                if (frequency == value)
                    return;
                frequency = value;
                OnPropertyChanged(nameof(Frequency));
            }
        }

        public int Occurrences
        {
            get { return occurrences; }
            set
            {
                if (occurrences == value)
                    return;
                occurrences = value;
                OnPropertyChanged(nameof(Occurrences));
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
    }*/
}

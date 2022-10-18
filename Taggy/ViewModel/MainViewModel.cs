﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Taggy.Model;

namespace Taggy.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private TagSelectionViewModel tagSelection = new();
        private ObservableCollection<Resource> resources = new();

        #endregion

        #region Constructors

        public MainViewModel()
        {
        }

        #endregion

        #region Properties

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

        public ObservableCollection<Resource> Resources
        {
            get { return resources; }
            set
            {
                if (resources == value)
                    return;
                resources = value;
                OnPropertyChanged(nameof(Resources));
            }
        }

        #endregion

        #region Actions

        public void AddResources(IEnumerable<Resource> addedResources)
        {
            foreach (var resource in addedResources)
                this.resources.Add(resource);
        }

        public void EditResources(IEnumerable<Resource> editedResources, Resource example)
        {
            foreach (var editedResource in editedResources)
            {
                if (string.IsNullOrWhiteSpace(example.Location) == false)
                    editedResource.Location = example.Location;
                if (example.Tags.Items.Any())
                    editedResource.Tags = example.Tags;
            }
        }

        public void RemoveResources(IEnumerable<Resource> removedResources)
        {
            var removedResourcesCollectionCopy = removedResources.ToList();
            foreach (var resource in removedResourcesCollectionCopy)
                this.resources.Remove(resource);
        }

        public void Load()
        {
            var resourceConfigFilePath = GetResourceConfigFilePath();
            if (File.Exists(resourceConfigFilePath))
            {
                var resourceConfig = new ResourceConfig();
                resourceConfig.LoadFile(resourceConfigFilePath);
                Resources = new ObservableCollection<Resource>(resourceConfig.Resources);
            }

            UpdateTags();
        }

        public void Save()
        {
            var resourceConfigFilePath = GetResourceConfigFilePath();
            var resourceConfig = new ResourceConfig();
            resourceConfig.Resources = Resources;
            resourceConfig.Save(resourceConfigFilePath);
        }

        private static string GetResourceConfigFilePath()
        {
            return "Resources.xml";
        }

        private void UpdateTags()
        {
            tagSelection.Items.Clear();
            var tags = resources.SelectMany(r => r.Tags.Items);
            var tagGroupes = tags.GroupBy(t => t.Category + "*" + t.Name);
            var tagElements = new List<TagSelectionElementViewModel>();
            var maxCount = (float)tagGroupes.Max(g => g.Count());

            foreach (var tagGroup in tagGroupes)
            {
                var tagElement = new TagSelectionElementViewModel();
                tagElement.Tag = new Tag(tagGroup.First().Category, tagGroup.First().Name);
                tagElement.Weight = 1 + (tagGroup.Count() - 1) / (float)maxCount;
                tagElements.Add(tagElement);
            }

            var weightedElements = tagElements.OrderByDescending(i => i.Weight);
            tagSelection.Items = new ObservableCollection<TagSelectionElementViewModel>(weightedElements);
        }

        #endregion

        #region Other Methods

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

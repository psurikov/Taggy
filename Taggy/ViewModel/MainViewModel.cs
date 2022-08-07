﻿using System;
using System.Collections.Generic;
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

        private TagCloudViewModel tagCloudView = new();
        private ObservableCollection<Resource> resources = new();

        #endregion

        #region Constructors

        public MainViewModel()
        {
        }

        #endregion

        #region Properties

        public TagCloudViewModel TagCloudView
        {
            get { return tagCloudView; }
            set
            {
                if (tagCloudView == value)
                    return;
                tagCloudView = value;
                OnPropertyChanged(nameof(TagCloudView));
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

        public void AddResources()
		{

		}

        public void RemoveResources()
		{

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

            /* temp */
            if (resources.Count == 0)
            {
                resources.Add(new Resource("http://localhost", new Tags() { Items = new List<Tag>() { new Tag("TestResource"), new Tag("Simple") } }, DateTime.Now));
                resources.Add(new Resource("http://youtube", new Tags() { Items = new List<Tag>() { new Tag("TestResource"), new Tag("VideoStorage") } }, DateTime.Now));
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

        public void Reindex()
        {
            /*var fileReferences = FileReferenceBrowser.Browse(Location);
            this.FileReferences = new ObservableCollection<FileReference>(fileReferences);

            var concatenatedTags = fileReferences.SelectMany(f => f.TagCluster.Items);
            var distinctTags = concatenatedTags.Distinct().OrderBy(t => t.Category + "#" + t.Value);
            Tags = new ObservableCollection<Tag>(distinctTags);
            foreach(var tag in tags)
            {
                var tagCloudItem = new TagCloudItemViewModel();
                tagCloudItem.Tag = tag;
                tagCloudItem.Weight = concatenatedTags.Count(t => t == tag);
                tagCloud.Items.Add(tagCloudItem);
            }*/
        }

        private static string GetResourceConfigFilePath()
        {
            return "Resources.xml";
        }

        private void UpdateTags()
        {
            this.tagCloudView.Items.Clear();
            var tags = resources.SelectMany(r => r.Tags.Items);
            var tagsGrouped = tags.GroupBy(t => t.Category + "*" + t.Name); 
            var tagCloudItems = new List<TagCloudItemViewModel>();

            foreach (var tagsGroup in tagsGrouped)
            {
                var tagCloudItem = new TagCloudItemViewModel();
                tagCloudItem.Tag = new Tag(tagsGroup.First().Category, tagsGroup.First().Name);
                tagCloudItem.Weight = tags.Count();
                tagCloudItems.Add(tagCloudItem);
            }

            var weightedItems = tagCloudItems.OrderByDescending(i => i.Weight);
            this.tagCloudView.Items = new ObservableCollection<TagCloudItemViewModel>(weightedItems);
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

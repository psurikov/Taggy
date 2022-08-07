﻿using System.ComponentModel;
using Taggy.Model;

namespace Taggy.ViewModel
{
    public class TagCloudItemViewModel : INotifyPropertyChanged
    {
        private Tag tag;
        private int weight;
        private bool isSelected;

        #region Constructors

        public TagCloudItemViewModel()
        {
            tag = new Tag();
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

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected == value)
                    return;
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
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

﻿using System.Windows.Controls;
using System.Windows.Input;
using Taggy.ViewModel;

namespace Taggy.View
{
    /// <summary>
    /// Interaction logic for TagCloudItemView.xaml
    /// </summary>
    public partial class TagCloudItemView : UserControl
    {
        public TagCloudItemView()
        {
            InitializeComponent();
            PreviewMouseDown += TagCloudView_MouseDown;
        }

        private void TagCloudView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var tagCloudItem = DataContext as TagCloudItemViewModel;
            if (tagCloudItem != null)
                tagCloudItem.IsSelected = !tagCloudItem.IsSelected;
        }
    }
}

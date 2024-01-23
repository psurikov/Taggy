using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Taggy.ViewModel;

namespace Taggy.View
{
    public partial class TagSelectionElementView : UserControl
    {
        public TagSelectionElementView()
        {
            InitializeComponent();
            PreviewMouseDown += TagCloudView_MouseDown;
        }

        private void TagCloudView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is TagSelectionElementViewModel tagElement)
                tagElement.IsSelected = !tagElement.IsSelected;
        }
    }

    public class TagFontValueConverter : IValueConverter
    {
        private double defaultFontSize;

        public TagFontValueConverter()
        {
            defaultFontSize = GetDefaultFontSize();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double valueFactor)
            {
                return defaultFontSize * (1 + valueFactor);
            }

            return defaultFontSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static double GetDefaultFontSize()
        {
            var defaultFontSize = TextBlock.GetFontSize(new TextBlock());
            return defaultFontSize;
        }
    }
}

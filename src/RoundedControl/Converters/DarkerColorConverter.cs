using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RoundedControl.Converters
{
    class DarkerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = ((SolidColorBrush)value).Color;
            return new SolidColorBrush(color.MakeDarker(0.2));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}

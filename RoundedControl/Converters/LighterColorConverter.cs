using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RoundedControl.Converters
{
    class LighterColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = ((SolidColorBrush)value).Color;
            return new SolidColorBrush(color.MakeLighter(0.8));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}

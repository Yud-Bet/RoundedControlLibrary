using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RoundedControl.Converters
{
    class BorderThicknessToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;
            var ret = new Thickness(-thickness.Top / 2);
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}

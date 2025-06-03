using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Ecommerce.Mobile.Converters
{
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return !string.IsNullOrEmpty(strValue);
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
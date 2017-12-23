using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TravellerTracker.Converters
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var enumValue = value as Enum;

            return enumValue == null ? DependencyProperty.UnsetValue : enumValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

    }
}

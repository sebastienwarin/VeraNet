// -----------------------------------------------------------------------
// <copyright file="SwitchColorConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WpfConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Globalization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class SwitchColorConverter : IValueConverter
    {
        public static SwitchColorConverter Instance = new SwitchColorConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value == true)
            {
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class SecurityColorConverter : IValueConverter
    {
        public static SecurityColorConverter Instance = new SecurityColorConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value == true)
            {
                return new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}

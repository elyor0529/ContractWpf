using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Contract.Import.Converters
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : System.Convert.ToDouble(value).ToString("N");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double d;

            if (double.TryParse(value as string, out d))
            {
                return d;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}

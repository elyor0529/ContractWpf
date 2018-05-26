using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Contract.Import.Converters
{
    [ValueConversion(typeof(DateTime), typeof(String))]

    public class DateTimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;

                return date.ToString("dd/MM/yyyy");
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime resultDateTime;
                if (DateTime.TryParseExact(value.ToString(), "dd/MM/yyyy", null, DateTimeStyles.None, out resultDateTime))
                {
                    return resultDateTime;
                }
            }
            return DependencyProperty.UnsetValue;
        }

    }
}

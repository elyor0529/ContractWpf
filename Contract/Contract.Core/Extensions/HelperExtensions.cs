using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Linq;
using Contract.Core.Helpers;

namespace Contract.Core.Extensions
{
    public static class HelperExtensions
    {
        public static bool? AsBoolean(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                bool flag;
                if (bool.TryParse(value, out flag))
                {
                    return flag;
                }
                if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return null;
        }

        public static bool AsBoolean(this string value, bool @default)
        {
            if (!string.IsNullOrEmpty(value))
            {
                bool flag;
                if (bool.TryParse(value, out flag))
                {
                    return flag;
                }
                if (string.Equals(value, "on", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (string.Equals(value, "off", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }
            return @default;
        }

        public static DateTime AsDateTime(this object value)
        {
            DateTime time;
            if (DateTime.TryParse(value.AsString(), out time))
            {
                return time;
            }
            return DateTime.MinValue;
        }

        public static DateTime? AsDateTime(this string value)
        {
            DateTime time;
            if (!string.IsNullOrEmpty(value) &&
                DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {
                return time;
            }
            return null;
        }

        public static DateTime AsDateTime(this string value, DateTime @default)
        {
            DateTime time;
            if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out time))
            {
                return time;
            }
            return @default;
        }

        public static decimal? AsDecimal(this string value)
        {
            decimal num;
            if (!string.IsNullOrEmpty(value) &&
                decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            return null;
        }

        public static decimal AsDecimal(this object value, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(value.AsString(), out num))
            {
                return num;
            }
            return defaultValue;
        }

        public static decimal AsDecimal(this string value, decimal @default)
        {
            decimal num;
            if (!string.IsNullOrEmpty(value) && decimal.TryParse(value, out num))
            {
                return num;
            }
            return @default;
        }

        public static double? AsDouble(this string value)
        {
            double num;
            if (!string.IsNullOrEmpty(value) &&
                double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            return null;
        }

        public static double AsDouble(this object value, double defaultValue)
        {
            double num;
            if (double.TryParse(value.AsString(), out num))
            {
                return num;
            }
            return defaultValue;
        }

        public static double AsDouble(this string value, double @default)
        {
            double num;
            if (!string.IsNullOrEmpty(value) &&
                double.TryParse(value, NumberStyles.Float, CultureInfo.GetCultureInfo("en-US"), out num))
            {
                return num;
            }
            return @default;
        }

        public static Guid? AsGuid(this string value)
        {
            Guid guid;
            if (Guid.TryParse(value, out guid))
            {
                return guid;
            }
            return null;
        }

        public static int? AsInt(this string value)
        {
            int num;
            if (!string.IsNullOrEmpty(value) &&
                int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            return null;
        }

        public static int AsInt(this object value, int defaultValue)
        {
            int num;
            if (int.TryParse(value.AsString(), out num))
            {
                return num;
            }
            return defaultValue;
        }

        public static int AsInt(this string value, int @default)
        {
            int num;
            if (!string.IsNullOrEmpty(value) && int.TryParse(value, out num))
            {
                return num;
            }
            return @default;
        }

        public static long AsLong(this object value, long defaultValue)
        {
            long num;
            if (long.TryParse(value.AsString(), out num))
            {
                return num;
            }
            return defaultValue;
        }

        public static long? AsLongInt(this string value)
        {
            long num;
            if (!string.IsNullOrEmpty(value) &&
                long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            return null;
        }

        public static long AsLongInt(this string value, long @default)
        {
            long num;
            if (!string.IsNullOrEmpty(value) && long.TryParse(value, out num))
            {
                return num;
            }
            return @default;
        }

        public static short AsShort(this object value, short defaultValue)
        {
            short num;
            if (short.TryParse(value.AsString(), out num))
            {
                return num;
            }
            return defaultValue;
        }

        public static string AsString(this object value)
        {
            return ((value == null) ? null : value.ToString());
        }

        public static string AsString(this object value, string format)
        {
            if (value == null)
            {
                return null;
            }
            return string.Format(string.Format("{{0:{0}}}", format), value);
        }

        public static string AsString(this object value, string format, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                return null;
            }
            return string.Format(formatProvider, string.Format("{{0:{0}}}", format), value);
        }

        public static XElement AsXmlElement(this Exception ex)
        {
            if (ex == null)
            {
                return null;
            }
            var element = new XElement("Exception");
            if (!string.IsNullOrWhiteSpace(ex.Message))
            {
                element.Add(new XElement("Message", ex.Message));
            }
            if (!string.IsNullOrWhiteSpace(ex.Source))
            {
                element.Add(new XElement("Source", ex.Source));
            }
            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                element.Add(new XElement("StackTrace", ex.StackTrace));
            }
            if (ex.InnerException != null)
            {
                element.Add(new XElement("InnerException", ex.InnerException.AsXmlElement()));
            }
            return element;
        }

        public static bool HasValue(this object value)
        {
            return (value.AsString().Length > 0);
        }

        public static bool HasValue(this string value)
        {
            return ((value != null) && (value.Length > 0));
        }
 
        public static T[,] ToMultiDeminsion<T>(this T[][] jArray)
        {
            var length = jArray.Length;
            var num2 = 0;
            if (length > 0)
            {
                num2 = jArray[0].Length;
            }
            var localArray = new T[length, num2];
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < num2; j++)
                {
                    localArray[i, j] = jArray[i][j];
                }
            }
            return localArray;
        }

        public static bool TryCast<T>(this object obj, out T target)
        {
            try
            {
                target = (T) obj;
                return true;
            }
            catch
            {
            }
            try
            {
                target = (T) Convert.ChangeType(obj, typeof (T));
                return true;
            }
            catch
            {
            }
            target = default(T);
            return false;
        }

        public static T CastEnum<T>(this object value) where T : struct
        {
            if (value == null)
                throw ErrorHelper.InvalidArgument("You passed null");
            if (value.GetType() == typeof (int))
                return (T) value;
            if (value.GetType() == typeof (string))
            {
                var intVal = ((string) value).AsInt();
                if (intVal.HasValue)
                    return (T) (object) intVal.Value;
                return (T) Enum.Parse(typeof (T), (string) value, true);
            }
            throw ErrorHelper.Failed("Failed to convert");
        }

        public static TResult ConvertEnum<TSource, TResult>(this TSource source)
            where TSource : struct
            where TResult : struct
        {
            if (!typeof (TSource).IsEnum || !typeof (TResult).IsEnum)
                throw ErrorHelper.InvalidOperation("Operation must be carried out between enums only");
            return (TResult) (object) (int) (object) source;
        }

        public static TResult? ConvertNullableEnum<TSource, TResult>(this TSource? source)
            where TSource : struct
            where TResult : struct
        {
            if (!typeof (TSource).IsEnum || !typeof (TResult).IsEnum)
                throw ErrorHelper.InvalidOperation("Operation must be carried out between enums only");
            if (!source.HasValue)
                return null;
            return source.Value.ConvertEnum<TSource, TResult>();
        }

        public static string ToText(this byte[] input)
        {
            if (input == null)
                throw ErrorHelper.ArgNull("input");
            return Encoding.UTF8.GetString(input);
        }

        public static byte[] ToByte(this string input)
        {
            return input.ToByte(Encoding.UTF8);
        }

        public static byte[] ToByte(this string input, Encoding encoding)
        {
            Guard.NotNull(input, "input");
            Guard.NotNull(encoding, "encoding");
            return encoding.GetBytes(input);
        }
    }
}
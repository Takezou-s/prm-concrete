using Promax.Core;
using Promax.Entities;
using System;
using System.Globalization;
using Utility.Binding;

namespace Promax.UI.Controllers.Converters
{
    public class SiloScalifyConverter : IBeeValueConverter
    {
        private FuncConverter _converter;

        public SiloScalifyConverter(Silo Silo)
        {
            _converter = new FuncConverter(x =>
            {
                short value = 0;
                value = ((double)x).Scalify(Silo.Scale);
                return value;
            }, x =>
            {
                double value = 0;
                value = System.Convert.ToDouble(x).Descalify(Silo.Scale);
                //var a = Math.Pow(10, Silo.Scale);
                //double asDouble = (double)x;
                //value=asDouble / a;
                return value;
            });
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IBeeValueConverter)_converter).Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IBeeValueConverter)_converter).ConvertBack(value, targetType, parameter, culture);
        }
    }
}

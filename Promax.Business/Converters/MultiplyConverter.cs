using System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Business
{
    public class MultiplyConverter : IBeeValueConverter
    {
        private FuncConverter _converter;
        public double Number { get; set; }

        public MultiplyConverter(double number)
        {
            Number = number;
            _converter = new FuncConverter(x =>
            {
                short value = 0;
                double result = ((double)x) * Number;
                value = System.Convert.ToInt16(result);
                return value;
            }, x =>
            {
                double value = 0;
                value = System.Convert.ToDouble(x);
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

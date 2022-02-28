using Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.UI.Controllers.Converters
{
    public class BoolifiedStringToShortConverter : IBeeValueConverter
    {
        private FuncConverter _converter;

        public BoolifiedStringToShortConverter()
        {
            _converter = new FuncConverter(x =>
            {
                short value = 0;
                x.Do(y => (y as String).Do(z => value = z.GetBool() ? System.Convert.ToInt16(1) : System.Convert.ToInt16(0)));
                return value;
            }, x =>
            {
                string value = "false";
                x.Do(y => value = y.IsEqual(System.Convert.ToInt16(0)) ? "true" : "false");
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

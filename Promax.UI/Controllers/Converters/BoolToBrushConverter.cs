using System;
using System.Globalization;
using System.Windows.Data;

namespace Promax.UI.Controllers.Converters
{
    class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            bool? val = value as bool?;
            if (val == null)
                return result;
            result = val == false ? "#FFDC1414" : "#FF32E949";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

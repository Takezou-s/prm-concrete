using System;
using System.Globalization;
using System.Windows.Data;

namespace Promax.UI
{
    class ConsistencyIndexToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            string consistency = value as string;
            if (consistency == null)
                return result;
            if (consistency == "H")
                result = 0;
            if (consistency == "N")
                result = 1;
            if (consistency == "A")
                result = 2;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            int? index = value as int?;
            if (index == null)
                return result;
            if (index == 0)
                result = "H";
            if (index == 1)
                result = "N";
            if (index == 2)
                result = "A";
            return result;
        }
    }
}

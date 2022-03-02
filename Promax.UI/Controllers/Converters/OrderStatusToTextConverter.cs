using System;
using System.Globalization;
using System.Windows.Data;

namespace Promax.UI
{
    class OrderStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            int? stockCatNum = value as int?;
            if (stockCatNum == null)
                return result;
            if (stockCatNum == 0)
                result = Strings.OnayBekleniyor;
            if (stockCatNum == 1)
                result = Strings.Onaylandı;
            if (stockCatNum == 2)
                result = Strings.Reddedildi;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}

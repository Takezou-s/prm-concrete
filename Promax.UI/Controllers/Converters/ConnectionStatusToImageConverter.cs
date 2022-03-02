using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Promax.UI
{
    public class ConnectionStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            bool? stockCatNum = value as bool?;
            if (stockCatNum == null)
                return result;
            if (stockCatNum == true)
                result = new Uri("/Promax.UI;component/Pictures/Icons/greenloader.png", UriKind.Relative);
            else
                result = new Uri("/Promax.UI;component/Pictures/Icons/redloader.png", UriKind.Relative);
            return new BitmapImage(result as Uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Promax.UI
{
    class ProductionStatusToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            int? stockCatNum = value as int?;
            if (stockCatNum == null)
                return result;
            if (stockCatNum == 0)
                result = new Uri("/Promax.UI;component/Pictures/Icons/redProd.png", UriKind.Relative);
            if (stockCatNum == 1)
                result = new Uri("/Promax.UI;component/Pictures/Icons/yellowProd.png", UriKind.Relative);
            if (stockCatNum >= 2)
                result = new Uri("/Promax.UI;component/Pictures/Icons/greenProd.png", UriKind.Relative);
            return new BitmapImage(result as Uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Promax.UI
{
    class StockTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            int? stockCatNum = value as int?;
            if (stockCatNum == null)
                return result;
            if (stockCatNum == 1)
                result = new Uri("/Promax.UI;component/Pictures/Icons/kare_agrega.png", UriKind.Relative);
            if (stockCatNum == 2)
                result = new Uri("/Promax.UI;component/Pictures/Icons/kare_cimento.png", UriKind.Relative);
            if (stockCatNum == 3)
                result = new Uri("/Promax.UI;component/Pictures/Icons/kare_su.png", UriKind.Relative);
            if (stockCatNum == 4)
                result = new Uri("/Promax.UI;component/Pictures/Icons/kare_katkı.png", UriKind.Relative);
            return new BitmapImage(result as Uri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -1;
        }
    }
}

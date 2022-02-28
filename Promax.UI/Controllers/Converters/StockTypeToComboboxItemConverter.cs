using System;
using System.Globalization;
using System.Windows.Data;

namespace Promax.UI.Controllers.Converters
{
    class StockTypeToComboboxItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object index = null;
            int? stockType = value as int?;
            if (stockType == null)
                return index;
            index = --stockType;
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object stockType = null;
            int? index = value as int?;
            if (index == null)
                return stockType;
            stockType = ++index;
            return stockType;
        }
    }
}

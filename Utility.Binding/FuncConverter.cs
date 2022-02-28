using System;
using System.Globalization;

namespace Utility.Binding
{
    public class FuncConverter : IBeeValueConverter
    {
        Func<object, object> _convertFunc;
        Func<object, object> _convertBackFunc;

        public FuncConverter()
        {
        }

        public FuncConverter(Func<object, object> convertFunc, Func<object, object> convertBackFunc)
        {
            _convertFunc = convertFunc;
            _convertBackFunc = convertBackFunc;
        }

        public FuncConverter ConvertFunction(Func<object, object> func)
        {
            _convertFunc = func;
            return this;
        }
        public FuncConverter ConvertBackFunction(Func<object, object> func)
        {
            _convertBackFunc = func;
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _convertFunc(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _convertBackFunc(value);
        }
    }
}

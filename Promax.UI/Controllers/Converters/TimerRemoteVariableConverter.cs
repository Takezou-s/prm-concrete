using Extensions;
using System;
using System.Globalization;
using Utility.Binding;

namespace Promax.UI.Controllers.Converters
{
    public class TimerRemoteVariableConverter : IBeeValueConverter
    {
        private FuncConverter _converter;
        /// <summary>
        /// 100 ms zaman sabitine göre değeri değiştirir. RemoteVariable Target tarafındadır.
        /// </summary>
        public TimerRemoteVariableConverter() : this(1000, 100, false)
        {

        }
        /// <summary>
        /// Zaman sabitine göre değeri değiştirir. RemoteVariable Target tarafındadır.
        /// </summary>
        /// <param name="değerSüreSabiti">ms cinsinden girilen değerin birimi.</param>
        public TimerRemoteVariableConverter(int değerSüreSabiti) : this(değerSüreSabiti, 100, false)
        {
        }
        /// <summary>
        /// Zaman sabitine göre değeri değiştirir. RemoteVariable Target tarafındadır.
        /// </summary>
        /// <param name="değerSüreSabiti">ms cinsinden girilen değerin birimi.</param>
        /// <param name="zamanSabiti">ms cinsinden zaman sabiti.</param>
        public TimerRemoteVariableConverter(int değerSüreSabiti, int zamanSabiti) : this(değerSüreSabiti, zamanSabiti, false)
        {
        }
        /// <summary>
        /// Zaman sabitine göre değeri değiştirir.
        /// </summary>
        /// <param name="değerSüreSabiti">ms cinsinden girilen değerin birimi.</param>
        /// <param name="zamanSabiti">ms cinsinden zaman sabiti.</param>
        /// <param name="variableIsSource">Değişken source mu, target mı? Source ise true.</param>
        public TimerRemoteVariableConverter(int değerSüreSabiti, int zamanSabiti, bool variableIsSource)
        {
            _converter = new FuncConverter(x =>
            {
                object value = null;
                variableIsSource.DoIfElse(y => y, y => value = ConvertValueFromRemoteVariable(x, zamanSabiti, değerSüreSabiti), y => value = ConvertValueToRemoteVariable(x, zamanSabiti, değerSüreSabiti));
                return value;
            }, x =>
            {
                object value = null;
                variableIsSource.DoIfElse(y => y, y => value = ConvertValueToRemoteVariable(x, zamanSabiti, değerSüreSabiti), y => value = ConvertValueFromRemoteVariable(x, zamanSabiti, değerSüreSabiti));
                return value;
            });
        }
        private object ConvertValueToRemoteVariable(object x, int zamanSabiti, int değerSüreSabiti)
        {
            short value = 0;
            double y = değerSüreSabiti;
            y /= zamanSabiti;
            y *= System.Convert.ToDouble(x);
            value = System.Convert.ToInt16(y);
            return value;
        }
        private object ConvertValueFromRemoteVariable(object x, int zamanSabiti, int değerSüreSabiti)
        {
            short value = 0;
            double y = değerSüreSabiti;
            y /= zamanSabiti;
            y = 1 / y;
            y *= System.Convert.ToDouble(x);
            value = System.Convert.ToInt16(y);
            return value;
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

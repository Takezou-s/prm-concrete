using Extensions;
using System.Collections.Generic;
using Utility.Binding;

namespace Promax.Core
{
    public class ConverterContainer
    {
        private Dictionary<string, IBeeValueConverter> _variablePropertyConverters = new Dictionary<string, IBeeValueConverter>();
        public ConverterContainer SetConverter(string propertyName, IBeeValueConverter converter)
        {
            if (!_variablePropertyConverters.ContainsKey(propertyName))
                _variablePropertyConverters.Add(propertyName, converter);
            _variablePropertyConverters[propertyName] = converter;
            return this;
        }
        public IBeeValueConverter GetConverter(string propertyName)
        {
            IBeeValueConverter result = null;
            _variablePropertyConverters.DoIf(x => x.ContainsKey(propertyName), x => result = x[propertyName]);
            return result;
        }
    }
}

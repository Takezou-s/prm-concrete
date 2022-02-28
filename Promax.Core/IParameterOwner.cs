using System.Collections.Generic;
using Utility.Binding;

namespace Promax.Core
{
    public interface IParameterOwner
    {
        IParameterOwner SetConverter(string propertyName, IBeeValueConverter converter);
        string ParameterOwnerName { get; set; }
        IBeeValueConverter GetConverterForVariable(string propertyName);
        IEnumerable<string> Parameters { get; }
    }
}

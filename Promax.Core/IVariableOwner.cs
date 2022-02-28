using System.Collections.Generic;
using Utility.Binding;

namespace Promax.Core
{
    public interface IVariableOwner
    {
        IVariableOwner SetConverter(string propertyName, IBeeValueConverter converter);
        string VariableOwnerName { get; set; }
        IBeeValueConverter GetConverterForVariable(string propertyName);
        IEnumerable<string> Variables { get; }
    }
}

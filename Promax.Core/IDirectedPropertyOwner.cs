using System.Collections.Generic;

namespace Promax.Core
{
    public interface IDirectedPropertyOwner
    {
        IReadOnlyDictionary<string, IEnumerable<string>> DirectedProperties { get; }
    }
}

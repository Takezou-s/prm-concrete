using System.Collections.Generic;

namespace Promax.Core
{
    public interface IMyEntity
    {
        IEnumerable<string> EntityProperties { get; }
    }
}
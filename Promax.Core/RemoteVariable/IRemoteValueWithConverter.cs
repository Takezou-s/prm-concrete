using RemoteVariableHandler.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Core
{
    public interface IRemoteValueWithConverter : IRemoteValue
    {
        IBeeValueConverter ReadConverter { get; set; }
        IBeeValueConverter WriteConverter { get; set; }
    }
}

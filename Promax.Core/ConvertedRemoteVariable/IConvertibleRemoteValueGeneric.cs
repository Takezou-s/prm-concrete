using RemoteVariableHandler.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Core
{
    public interface IConvertibleRemoteValue<T> : IConvertibleRemoteValue
    {
        new T ConvertedWriteValue { get; set; }
        new T ConvertedReadValue { get; set; }
    }
}

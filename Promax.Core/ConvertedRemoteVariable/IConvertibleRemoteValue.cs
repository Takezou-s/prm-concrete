using RemoteVariableHandler.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Core
{
    public interface IConvertibleRemoteValue : INotifyPropertyChanged
    {
        object ConvertedWriteValue { get; set; }
        object ConvertedReadValue { get; set; }
        IRemoteValue RemoteValue { get; }
    }
}

using System.ComponentModel;

namespace RemoteVariableHandler.Core
{
    public interface IRemoteValue : INotifyPropertyChanged
    {
        object WriteValue { get; set; }
        object ReadValue { get; set; }
    }
}

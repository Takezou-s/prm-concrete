using RemoteVariableHandler.Core;

namespace Promax.Core
{
    public interface IConvertibleRemoteVariable : IConvertibleRemoteValue
    {
        IRemoteVariable RemoteVariable { get; }
    }
}

using RemoteVariableHandler.Core;

namespace Promax.Core
{
    public interface IRemoteVariableWithConverter<T> : IRemoteValueWithConverter<T>, IRemoteVariable<T>
    {

    }
}

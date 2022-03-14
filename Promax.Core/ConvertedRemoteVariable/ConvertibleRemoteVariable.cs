using RemoteVariableHandler.Core;
using Utility.Binding;

namespace Promax.Core
{
    public class ConvertibleRemoteVariable<T> : ConvertibleRemoteValue<T>, IConvertibleRemoteVariable<T>
    {
        public IRemoteVariable RemoteVariable => (IRemoteVariable)RemoteValue;

        public ConvertibleRemoteVariable(IRemoteVariable remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter) : base(remoteValue, readConverter, writeConverter)
        {
        }
    }
}

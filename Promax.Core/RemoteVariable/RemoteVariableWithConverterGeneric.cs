using RemoteVariableHandler.Core;
using System;
using Utility.Binding;

namespace Promax.Core
{
    public class RemoteVariableWithConverter<T> : RemoteVariableWithConverter, IRemoteVariableWithConverter<T>
    {
        protected IRemoteVariable<T> _remoteVariableGeneric { get => (IRemoteVariable<T>)base._remoteVariable; set => base._remoteVariable = value; }
        public RemoteVariableWithConverter(IRemoteVariable<T> remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter) : base(remoteValue, readConverter, writeConverter)
        {
        }
        T IRemoteValue<T>.WriteValue { get => (T)Convert.ChangeType(WriteValue, typeof(T)); set => WriteValue = value; }
        T IRemoteValue<T>.ReadValue { get => (T)Convert.ChangeType(ReadValue, typeof(T)); set => ReadValue = value; }
    }
}

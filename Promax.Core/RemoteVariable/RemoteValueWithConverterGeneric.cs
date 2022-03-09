using RemoteVariableHandler.Core;
using Utility.Binding;

namespace Promax.Core
{
    public class RemoteValueWithConverter<T> : RemoteValueWithConverter, IRemoteValueWithConverter<T>
    {
        public RemoteValueWithConverter(IRemoteValue<T> remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter) : base(remoteValue, readConverter, writeConverter)
        {
        }

        protected new virtual IRemoteValue<T> _remoteValue { get => (IRemoteValue<T>)base._remoteValue; set => base._remoteValue = value; }
        T IRemoteValue<T>.WriteValue { get => _remoteValue.WriteValue; set => _remoteValue.WriteValue = GetConvertedValue<T>(value, WriteConverter); }
        T IRemoteValue<T>.ReadValue { get => _remoteValue.ReadValue; set => _remoteValue.ReadValue = GetConvertedValue<T>(value, ReadConverter); }
    }
}

using Extensions;
using RemoteVariableHandler.Core;
using System;
using System.ComponentModel;
using Utility.Binding;

namespace Promax.Core
{
    public class RemoteValueWithConverter : IRemoteValueWithConverter
    {
        protected virtual IRemoteValue _remoteValue { get; set; }

        public RemoteValueWithConverter(IRemoteValue remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter)
        {
            _remoteValue = remoteValue;
            ReadConverter = readConverter;
            WriteConverter = writeConverter;
        }

        public IBeeValueConverter ReadConverter {get;set;}
        public IBeeValueConverter WriteConverter {get;set;}
        public object WriteValue { get => _remoteValue.WriteValue; set => _remoteValue.WriteValue = GetConvertedValue(value, WriteConverter); }
        public object ReadValue { get => _remoteValue.ReadValue; set => _remoteValue.ReadValue = GetConvertedValue(value, ReadConverter); }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _remoteValue.PropertyChanged += value;
            }

            remove
            {
                _remoteValue.PropertyChanged -= value;
            }
        }

        protected virtual object GetConvertedValue(object value, IBeeValueConverter converter)
        {
            object result = value;
            converter.Do(x => result = x.Convert(value, null, null, null));
            return result;
        }
        protected virtual T GetConvertedValue<T>(object value, IBeeValueConverter converter)
        {
            object result = value;
            converter.Do(x => result = x.Convert(value, null, null, null));
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}

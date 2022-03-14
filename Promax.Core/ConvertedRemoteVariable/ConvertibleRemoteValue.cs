using Extensions;
using RemoteVariableHandler.Core;
using System;
using System.ComponentModel;
using Utility.Binding;

namespace Promax.Core
{
    public class ConvertibleRemoteValue<T> : IConvertibleRemoteValue<T>
    {
        private MyBinding _binding = new MyBinding();

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private T _convertedWriteValue;
        private T _convertedReadValue;
        private IRemoteValue _remoteValue;
        #endregion
        #region PropertiesByAutoPropCreator
        public T ConvertedWriteValue
        {
            get
            {
                return _convertedWriteValue;
            }
            set
            {
                bool changed = false;
                if (!_convertedWriteValue.IsEqual(value))
                    changed = true;
                _convertedWriteValue = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ConvertedWriteValue));
                }
            }
        }
        public T ConvertedReadValue
        {
            get
            {
                return _convertedReadValue;
            }
            set
            {
                bool changed = false;
                if (!_convertedReadValue.IsEqual(value))
                    changed = true;
                _convertedReadValue = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(ConvertedReadValue));
                }
            }
        }
        public IRemoteValue RemoteValue
        {
            get
            {
                return _remoteValue;
            }
           protected set
            {
                bool changed = false;
                if (!_remoteValue.IsEqual(value))
                    changed = true;
                _remoteValue = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(RemoteValue));
                }
            }
        }
        #endregion
        object IConvertibleRemoteValue.ConvertedWriteValue 
        { 
            get
            {
                return ConvertedWriteValue;
            }
            set
            {
                try
                {
                    ConvertedWriteValue = (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }
        object IConvertibleRemoteValue.ConvertedReadValue
        {
            get
            {
                return ConvertedReadValue;
            }
            set
            {
                try
                {
                    ConvertedReadValue = (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException("Değişken gereken tipte değil!");
                }
            }
        }

        public ConvertibleRemoteValue(IRemoteValue remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter)
        {
            RemoteValue = remoteValue;
            _binding.CreateBinding().Source(RemoteValue).SourceProperty(nameof(RemoteValue.ReadValue)).
                Target(this).TargetProperty(nameof(ConvertedReadValue)).
                Convert(readConverter).Behaviour(MyBindingBehaviour.Map).Mode(MyBindingMode.OneWay);
            _binding.CreateBinding().Target(RemoteValue).TargetProperty(nameof(RemoteValue.WriteValue)).
                Source(this).SourceProperty(nameof(ConvertedWriteValue)).
                Convert(writeConverter).Behaviour(MyBindingBehaviour.Map).Mode(MyBindingMode.OneWay);
            _binding.InitialMapping();
        }
    }
}

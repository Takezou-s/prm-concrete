using RemoteVariableHandler.Core;
using System;
using System.ComponentModel;
using Utility.Binding;

namespace Promax.Core
{
    public class RemoteVariableWithConverter : IRemoteVariableWithConverter
    {
        protected RemoteValueWithConverter _remoteValueWithConverter;
        protected IRemoteVariable _remoteVariable;

        public RemoteVariableWithConverter(IRemoteVariable remoteValue, IBeeValueConverter readConverter, IBeeValueConverter writeConverter)
        {
            _remoteVariable = remoteValue;
            _remoteValueWithConverter = new RemoteValueWithConverter(_remoteVariable, readConverter, writeConverter);
        }

        public IBeeValueConverter ReadConverter { get => ((IRemoteValueWithConverter)_remoteValueWithConverter).ReadConverter; set => ((IRemoteValueWithConverter)_remoteValueWithConverter).ReadConverter = value; }
        public IBeeValueConverter WriteConverter { get => ((IRemoteValueWithConverter)_remoteValueWithConverter).WriteConverter; set => ((IRemoteValueWithConverter)_remoteValueWithConverter).WriteConverter = value; }
        public object WriteValue { get => ((IRemoteValue)_remoteValueWithConverter).WriteValue; set => ((IRemoteValue)_remoteValueWithConverter).WriteValue = value; }
        public object ReadValue { get => ((IRemoteValue)_remoteValueWithConverter).ReadValue; set => ((IRemoteValue)_remoteValueWithConverter).ReadValue = value; }
        public string VariableName { get => _remoteVariable.VariableName; set => _remoteVariable.VariableName = value; }

        public Type Type => _remoteVariable.Type;

        public IVariableDefinition Definition { get => _remoteVariable.Definition; set => _remoteVariable.Definition = value; }
        public IVariableCommunicator Communicator { get => _remoteVariable.Communicator; set => _remoteVariable.Communicator = value; }

        public event EventHandler ReadValueChanged
        {
            add
            {
                _remoteVariable.ReadValueChanged += value;
            }
            remove
            {
                _remoteVariable.ReadValueChanged -= value;
            }
        }
        public event EventHandler WriteValueChanged
        {
            add
            {
                _remoteVariable.WriteValueChanged += value;
            }
            remove
            {
                _remoteVariable.WriteValueChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ((INotifyPropertyChanged)_remoteValueWithConverter).PropertyChanged += value;
            }

            remove
            {
                ((INotifyPropertyChanged)_remoteValueWithConverter).PropertyChanged -= value;
            }
        }
    }
}

using RemoteVariableHandler.Core;
using System;
using System.ComponentModel;
using System.Text;

namespace Promax.Business
{
    public class BitOfShortVariable : IRemoteVariable<bool>
    {
        private IRemoteVariable<bool> _variable;
        private BitOfShortVariableDefinition _definition;

        public BitOfShortVariable()
        {
            _variable = new RemoteVariable<bool>();
            Definition = new BitOfShortVariableDefinition();
        }
        public BitOfShortVariable(ushort register, int bitNumber) : this()
        {
            Register = register;
            BitNumber = bitNumber;
        }
        public ushort Register { get => Definition.Register; set => Definition.Register = value; }
        public int BitNumber { get => Definition.BitNumber; set => Definition.BitNumber = value; }

        public BitOfShortVariableDefinition Definition
        {
            get => _definition; set
            {
                _definition = value;
                (this as IRemoteVariable).Definition = value;
            }
        }

        public bool WriteValue { get => _variable.WriteValue; set => _variable.WriteValue = value; }
        public bool ReadValue { get => _variable.ReadValue; set => _variable.ReadValue = value; }
        public string VariableName { get => _variable.VariableName; set => _variable.VariableName = value; }

        public Type Type => _variable.Type;

        IVariableDefinition IRemoteVariable.Definition { get => _variable.Definition; set => _variable.Definition = value; }
        public IVariableCommunicator Communicator { get => _variable.Communicator; set => _variable.Communicator = value; }
        object IRemoteValue.WriteValue { get => ((IRemoteVariable)_variable).WriteValue; set => ((IRemoteVariable)_variable).WriteValue = value; }
        object IRemoteValue.ReadValue { get => ((IRemoteVariable)_variable).ReadValue; set => ((IRemoteVariable)_variable).ReadValue = value; }

        public event EventHandler ReadValueChanged
        {
            add
            {
                _variable.ReadValueChanged += value;
            }

            remove
            {
                _variable.ReadValueChanged -= value;
            }
        }

        public event EventHandler WriteValueChanged
        {
            add
            {
                _variable.WriteValueChanged += value;
            }

            remove
            {
                _variable.WriteValueChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _variable.PropertyChanged += value;
            }

            remove
            {
                _variable.PropertyChanged -= value;
            }
        }
    }
}

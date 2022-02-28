using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    [Serializable]
    public class ModbusVariable<T> : IModbusVariable<T>
    {
        private IRemoteVariable<T> _variable;
        private IRegisterNumberedVariableDefinition definition;

        public ModbusVariable()
        {
            _variable = new RemoteVariable<T>();
        }
        public ModbusVariable(ushort register) : this()
        {
            Register = register;
        }
        public ModbusVariable(ushort register, T writeValue) : this(register)
        {
            WriteValue = writeValue;
        }
        public ushort Register
        {
            get
            {
                ushort result = Definition == null ? (ushort)0 : Definition.Register;
                return result;
            }
            set
            {
                Definition = new RegisterNumberedVariableDefinition() { Register = value };
            }
        }
        public IRegisterNumberedVariableDefinition Definition
        {
            get => definition; set
            {
                definition = value;
                (this as IRemoteVariable).Definition = value;
            }
        }
        public string VariableName { get => _variable.VariableName; set => _variable.VariableName = value; }
        public T WriteValue { get => _variable.WriteValue; set => _variable.WriteValue = value; }
        public T ReadValue { get => _variable.ReadValue; set => _variable.ReadValue = value; }
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
        public Type Type => _variable.Type;

        public IVariableCommunicator Communicator { get => ((IRemoteVariable)_variable).Communicator; set => ((IRemoteVariable)_variable).Communicator = value; }
        object IRemoteValue.WriteValue { get => ((IRemoteVariable)_variable).WriteValue; set => ((IRemoteVariable)_variable).WriteValue = value; }
        object IRemoteValue.ReadValue { get => ((IRemoteVariable)_variable).ReadValue; set => ((IRemoteVariable)_variable).ReadValue = value; }
        IVariableDefinition IRemoteVariable.Definition { get => ((IRemoteVariable)_variable).Definition; set => ((IRemoteVariable)_variable).Definition = value; }

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
    }
}

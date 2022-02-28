using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    public class ModbusVariableBuilder : IModbusVariableBuilder
    {
        private VariableBuilder _builder = new VariableBuilder();

        public IRemoteVariable GetVariable<T>()
        {
            return _builder.GetVariable<T>();
        }

        public IRemoteVariable<T> GetVariableAsGeneric<T>()
        {
            return _builder.GetVariableAsGeneric<T>();
        }

        public IModbusVariableBuilder Reset()
        {
            _builder.Reset();
            return this;
        }

        public IModbusVariableBuilder SetDefinition(IVariableDefinition definition)
        {
            _builder.SetDefinition(definition);
            return this;
        }

        public IModbusVariableBuilder SetReadValue<T>(T value)
        {
            _builder.SetReadValue<T>(value);
            return this;
        }

        public IModbusVariableBuilder SetRegister(ushort register)
        {
            _builder.SetDefinition(new RegisterNumberedVariableDefinition() { Register = register });
            return this;
        }

        public IModbusVariableBuilder SetWriteValue<T>(T value)
        {
            _builder.SetWriteValue<T>(value);
            return this;
        }

        IVariableBuilder IVariableBuilder.Reset()
        {
            return _builder.Reset();
        }

        IVariableBuilder IVariableBuilder.SetDefinition(IVariableDefinition definition)
        {
            return SetDefinition(definition);
        }

        IVariableBuilder IVariableBuilder.SetReadValue<T>(T value)
        {
            return SetReadValue<T>(value);
        }

        IVariableBuilder IVariableBuilder.SetWriteValue<T>(T value)
        {
            return SetWriteValue<T>(value);
        }
    }
}

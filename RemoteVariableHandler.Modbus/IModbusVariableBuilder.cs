using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    public interface IModbusVariableBuilder : IVariableBuilder
    {
        new IModbusVariableBuilder SetDefinition(IVariableDefinition definition);
        new IModbusVariableBuilder SetWriteValue<T>(T value);
        new IModbusVariableBuilder SetReadValue<T>(T value);
        new IModbusVariableBuilder Reset();
        IModbusVariableBuilder SetRegister(ushort register);
    }
}

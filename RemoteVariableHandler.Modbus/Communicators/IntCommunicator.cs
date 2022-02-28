using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus.Communicators
{
    internal class IntCommunicator : SubModbusCommunicator, IVariableCommunicator
    {
        internal IntCommunicator(ushort slave, IModbusDriver modbusDriver)
        {
            _slave = slave;
            _modbusDriver = modbusDriver;
        }

        public void Read(IRemoteVariable variable)
        {
            ushort[] value = _modbusDriver.ReadRegisters(_slave, (ushort)variable.Definition.Definition, 2);
            variable.ReadValue = ModbusUtility.GetInt(value);
        }

        public async Task ReadAsync(IRemoteVariable variable)
        {
            ushort[] value = await _modbusDriver.ReadRegistersAsync(_slave, (ushort)variable.Definition.Definition, 2);
            variable.ReadValue = ModbusUtility.GetInt(value);
        }

        public void Write(IRemoteVariable variable)
        {
            _modbusDriver.WriteRegisters(_slave, (ushort)variable.Definition.Definition, ModbusUtility.SeparateToWords((int)variable.WriteValue));
        }

        public async Task WriteAsync(IRemoteVariable variable)
        {
            await _modbusDriver.WriteRegistersAsync(_slave, (ushort)variable.Definition.Definition, ModbusUtility.SeparateToWords((int)variable.WriteValue));
        }
    }
}

using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus.Communicators
{
    internal class ShortCommunicator : SubModbusCommunicator, IVariableCommunicator
    {
        internal ShortCommunicator(ushort slave, IModbusDriver modbusDriver)
        {
            _slave = slave;
            _modbusDriver = modbusDriver;
        }

        public void Read(IRemoteVariable variable)
        {
            ushort value = _modbusDriver.ReadRegister(_slave, (ushort)variable.Definition.Definition);
            variable.ReadValue = ModbusUtility.GetShort(value);
        }

        public async Task ReadAsync(IRemoteVariable variable)
        {
            ushort value = await _modbusDriver.ReadRegisterAsync(_slave, (ushort)variable.Definition.Definition);
            variable.ReadValue = ModbusUtility.GetShort(value);
        }

        public void Write(IRemoteVariable variable)
        {
            _modbusDriver.WriteRegister(_slave, (ushort)variable.Definition.Definition, ModbusUtility.GetUShort((short)variable.WriteValue));
        }

        public async Task WriteAsync(IRemoteVariable variable)
        {
            await _modbusDriver.WriteRegisterAsync(_slave, (ushort)variable.Definition.Definition, ModbusUtility.GetUShort((short)variable.WriteValue));
        }
    }
}

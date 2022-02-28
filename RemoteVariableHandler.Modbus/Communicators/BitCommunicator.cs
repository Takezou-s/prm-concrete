using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus.Communicators
{
    internal class BitCommunicator : SubModbusCommunicator, IVariableCommunicator
    {
        internal BitCommunicator(ushort slave, IModbusDriver modbusDriver)
        {
            _slave = slave;
            _modbusDriver = modbusDriver;
        }

        public void Read(IRemoteVariable variable)
        {
            bool value = _modbusDriver.ReadBit(_slave, (ushort)variable.Definition.Definition);
            variable.ReadValue = value;
        }

        public async Task ReadAsync(IRemoteVariable variable)
        {
            bool value = await _modbusDriver.ReadBitAsync(_slave, (ushort)variable.Definition.Definition);
            variable.ReadValue = value;
        }

        public void Write(IRemoteVariable variable)
        {
            _modbusDriver.WriteBit(_slave, (ushort)variable.Definition.Definition, (bool)variable.WriteValue);
        }

        public async Task WriteAsync(IRemoteVariable variable)
        {
            await _modbusDriver.WriteBitAsync(_slave, (ushort)variable.Definition.Definition, (bool)variable.WriteValue);
        }


    }
}

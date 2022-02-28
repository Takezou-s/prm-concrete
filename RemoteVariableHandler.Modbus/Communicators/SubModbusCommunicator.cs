using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus.Communicators
{
    internal abstract class SubModbusCommunicator
    {
        protected IModbusDriver _modbusDriver;
        protected ushort _slave;

        internal SubModbusCommunicator()
        {

        }

        internal SubModbusCommunicator(IModbusDriver modbusDriver, ushort slave)
        {
            _modbusDriver = modbusDriver;
            _slave = slave;
        }

        public void SetModbusDriver(IModbusDriver modbusDriver)
        {
            _modbusDriver = modbusDriver;
        }

        public void SetSlave(ushort slave)
        {
            _slave = slave;
        }
    }
}

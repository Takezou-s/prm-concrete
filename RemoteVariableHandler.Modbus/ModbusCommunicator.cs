using RemoteVariableHandler.Core;
using RemoteVariableHandler.Modbus.Communicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    [Serializable]
    public class ModbusCommunicator : VariableCommunicator
    {
        /// <summary>
        /// Haberleşmeyi yapan driver.
        /// </summary>
        private readonly IModbusDriver _modbusDriver;
        private readonly ushort _slave;
        public ModbusCommunicator(ushort slave, IModbusDriver modbusDriver) : base()
        {
            _modbusDriver = modbusDriver;
            _slave = slave;
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(bool), new BitCommunicator(slave, modbusDriver));
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(short), new ShortCommunicator(slave, modbusDriver));
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(ushort), new UShortCommunicator(slave, modbusDriver));
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(int), new IntCommunicator(slave, modbusDriver));
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(uint), new UIntCommunicator(slave, modbusDriver));
            SetCommunicatorForType(typeof(RegisterNumberedVariableDefinition), typeof(float), new FloatCommunicator(slave, modbusDriver));
        }
    }
}

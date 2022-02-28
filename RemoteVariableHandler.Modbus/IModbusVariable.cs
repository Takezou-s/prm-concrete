using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    public interface IModbusVariable<T> : IRemoteVariable<T>
    {
        ushort Register { get; set; }
        new IRegisterNumberedVariableDefinition Definition { get; set; }
    }
}

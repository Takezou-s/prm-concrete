using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    public class ModbusVariableDefinitionCreator : IVariableDefinitionCreator
    {
        public IVariableDefinition CreateDefinition(object definition)
        {
            if(definition != null)
            {
                try
                {
                    ushort register = (ushort)definition;
                    return new RegisterNumberedVariableDefinition() { Register = register };
                }
                catch
                {
                    throw new InvalidCastException("Tanım gereken tipte değil! Gereken tip: " + typeof(ushort));
                }
            }
            throw new NullReferenceException();
        }
    }
}

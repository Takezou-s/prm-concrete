using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Modbus
{
    [Serializable]
    public class RegisterNumberedVariableDefinition : IRegisterNumberedVariableDefinition
    {
        object IVariableDefinition.Definition
        {
            get
            {
                return Register;
            }
            set
            {
                try
                {
                    Register = (ushort)value;
                }
                catch
                {
                    throw new InvalidCastException("Tanım gereken tipte değil! Gereken tip: " + Register.GetType());
                }
            }
        }

        public ushort Register { get; set; }

        internal RegisterNumberedVariableDefinition()
        {

        }
    }
}

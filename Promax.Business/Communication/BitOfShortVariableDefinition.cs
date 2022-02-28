using RemoteVariableHandler.Core;
using System;

namespace Promax.Business
{
    public class BitOfShortVariableDefinition : IVariableDefinition
    {
        public object Definition
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
        public int BitNumber { get; set; }
    }
}

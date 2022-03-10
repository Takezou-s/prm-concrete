using Promax.Core;
using RemoteVariableHandler.Core;
using RemoteVariableHandler.Modbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Business
{
    public partial class VariableScope
    {
        private MyBinding _binding = new MyBinding();
        
        public IRemoteValue Var1 { get; set; }
        public IRemoteValue Var2 { get; set; }
        public IRemoteValue Var3 { get; set; }
        public IRemoteValue Var4 { get; set; }

        protected override void InitVariables()
        {
            Var1 = new RemoteValueWithConverter(new ModbusVariable<short>(0), new MultiplyConverter(10), new MultiplyConverter(10));
            Var2 = new RemoteValueWithConverter<short>(new ModbusVariable<short>(1), new MultiplyConverter(100), new MultiplyConverter(100));
            Var3 = new RemoteVariableWithConverter(new ModbusVariable<short>(2), new MultiplyConverter(1000), new MultiplyConverter(1000));
            Var4 = new RemoteVariableWithConverter<short>(new ModbusVariable<short>(3), new MultiplyConverter(10000), new MultiplyConverter(10000));
            _binding.InitialMapping();
        }
    }
}

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
        private MyBinding _bindings = new MyBinding();
        public IConvertibleRemoteVariable ConvertibleRemoteVariable { get; set; }
        public IRemoteVariable ConvertibleRemoteVariable_RemoteVariable { get; set; }

        public IRemoteVariable SetConvertibleRemoteVariable { get; set; }
        public IRemoteVariable ConvertibleRemoteVariable_Reflection { get; set; }
        protected override void InitVariables()
        {
            SetConvertibleRemoteVariable = new ModbusVariable<short>(2);
            ConvertibleRemoteVariable_Reflection = new ModbusVariable<double>(8);

            ConvertibleRemoteVariable_RemoteVariable = new ModbusVariable<short>(1);
            ConvertibleRemoteVariable = new ConvertibleRemoteVariable<double>(ConvertibleRemoteVariable_RemoteVariable, new MultiplyConverter(1.2), new MultiplyConverter(2.1));

            _bindings.CreateBinding().Source(SetConvertibleRemoteVariable).SourceProperty("ReadValue").Target(ConvertibleRemoteVariable_RemoteVariable).TargetProperty("ReadValue");
            _bindings.CreateBinding().Source(SetConvertibleRemoteVariable).SourceProperty("WriteValue").Target(ConvertibleRemoteVariable_RemoteVariable).TargetProperty("WriteValue");

            _bindings.CreateBinding().Source(ConvertibleRemoteVariable).SourceProperty("ConvertedReadValue").Target(ConvertibleRemoteVariable_Reflection).TargetProperty("ReadValue");
            _bindings.CreateBinding().Source(ConvertibleRemoteVariable).SourceProperty("ConvertedWriteValue").Target(ConvertibleRemoteVariable_Reflection).TargetProperty("WriteValue");

            _bindings.InitialMapping();
        }
    }
}

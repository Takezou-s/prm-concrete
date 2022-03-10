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

        protected override void InitVariables()
        {
            _binding.InitialMapping();
        }
    }
}

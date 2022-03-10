using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public partial class VariableScope : VariablesBase
    {
        public override string ScopeName { get; set; } = "VariableScope";
        public VariableScope(IVariableCommunicator communicator) : base(communicator)
        {

        }
    }
}

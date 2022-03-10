using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public partial class RetentiveParameterScope : VariablesBase
    {
        public override string ScopeName { get; set; } = "RetentiveParameterScope";
        public RetentiveParameterScope(IVariableCommunicator communicator) : base(communicator)
        {

        }
    }
}

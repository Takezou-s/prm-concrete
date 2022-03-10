using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public partial class RecipeScope : VariablesBase
    {
        public override string ScopeName { get; set; } = "RecipeScope";
        public RecipeScope(IVariableCommunicator communicator) : base(communicator)
        {

        }
    }
}

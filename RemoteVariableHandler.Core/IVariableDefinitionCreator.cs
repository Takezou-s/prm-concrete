using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public interface IVariableDefinitionCreator
    {
        IVariableDefinition CreateDefinition(object definition);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public interface IRemoteVariable:IRemoteValue
    {
        string VariableName { get; set; }
        Type Type { get; }
        IVariableDefinition Definition { get; set; }
        event EventHandler ReadValueChanged;
        event EventHandler WriteValueChanged;
        IVariableCommunicator Communicator { get; set; }
    }
}

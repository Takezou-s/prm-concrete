using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public interface IVariableCommunicator
    {
        void Read(IRemoteVariable variable);
        Task ReadAsync(IRemoteVariable variable);
        void Write(IRemoteVariable variable);
        Task WriteAsync(IRemoteVariable variable);
    }
}

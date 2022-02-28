using RemoteVariableHandler.Core;
using System;

namespace Promax.Business
{
    public class RemoteVariableException : Exception
    {
        public RemoteVariableException(IRemoteVariable variable, string operation)
        {
            Variable = variable;
            Operation = operation;
        }

        public RemoteVariableException(Action action, IRemoteVariable variable, string message, string operation, Exception innerException) : base(message, innerException)
        {
            Variable = variable;
            Action = action;
            Operation = operation;
        }

        public IRemoteVariable Variable { get; private set; }
        public Action Action { get; private set; }
        public string Operation { get; private set; }
    }
}

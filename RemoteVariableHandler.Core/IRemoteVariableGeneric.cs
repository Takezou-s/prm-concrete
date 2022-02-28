using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public interface IRemoteVariable<T> : IRemoteVariable, IRemoteValue<T>
    {
        //new T WriteValue { get; set; }
        //new T ReadValue { get; set; }
    }
}

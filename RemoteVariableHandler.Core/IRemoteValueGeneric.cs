using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public interface IRemoteValue<T> : IRemoteValue
    {
        new T WriteValue { get; set; }
        new T ReadValue { get; set; }
    }
}

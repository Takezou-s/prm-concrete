using System.Collections.Generic;

namespace Promax.Core
{
    public interface ICommander
    {
        string CommanderName { get; set; }
        IReadOnlyDictionary<string, MyCommand> Commands { get; }
        void InvokeCommand(string command, object[] parameters = null);
    }
}

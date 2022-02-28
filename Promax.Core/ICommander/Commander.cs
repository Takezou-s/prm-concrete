using Extensions;
using System.Collections.Generic;

namespace Promax.Core
{
    public class Commander : ICommander
    {
        private Dictionary<string, MyCommand> _commands = new Dictionary<string, MyCommand>();
        #region ICommander
        public string CommanderName { get; set; }

        public IReadOnlyDictionary<string, MyCommand> Commands => _commands;

        public void InvokeCommand(string commandName, object[] parameters = null)
        {
            _commands.DoIf(x => !string.IsNullOrEmpty(commandName) && x.ContainsKey(commandName), x => x[commandName].Invoke(parameters));
        }
        #endregion
        public void RegisterCommand(string commandName)
        {
            RegisterCommand(commandName, new MyCommand());
        }
        public void RegisterCommand(string commandName, MyCommand command)
        {
            _commands.DoIfElse(x => !string.IsNullOrEmpty(commandName) && !x.ContainsKey(commandName), x => x.Add(commandName, command), x => x[commandName] = command);
        }
    }
}

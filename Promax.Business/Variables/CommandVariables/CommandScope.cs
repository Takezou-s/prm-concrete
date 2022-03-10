using Extensions;
using Promax.Core;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace Promax.Business
{
    public partial class CommandScope : VariablesBase, ICommandScope
    {
        private MyBinding _internalBindings = new MyBinding();

        public override string ScopeName { get; set; } = "CommandScope";

        public CommanderContainer CommanderContainer { get; private set; }
        public CommandScope(IVariableCommunicator communicator, CommanderContainer commanderContainer) : base(communicator)
        {
            CommanderContainer = commanderContainer;
            CommanderContainer.ObjectRegistered += CommanderContainer_ObjectRegistered;
            SetCommands();
        }

        private void CommanderContainer_ObjectRegistered(object container, ICommander registeredObject)
        {
            registeredObject.Do(y => SetCommand(y));
        }

        public void SetCommand(ICommander commander)
        {
            foreach (var item in commander.Commands)
            {
                GetVariable(commander.CommanderName, item.Key).Do(x =>
                {
                    item.Value.Action = (parameters) =>
                    {
                        if (!(x is VariableWithExecute))
                        {
                            if (parameters != null && parameters.Length > 0 && parameters[0] != null)
                            {
                                ReflectionController.SetPropertyValue(x, nameof(x.WriteValue), parameters[0]);
                                //x.WriteValue = parameters[0];
                            }
                            x.Write();
                        }
                        else
                        {
                            var variableWithExec = x as VariableWithExecute;
                            if (parameters != null && parameters.Length > 0 && parameters[0] != null)
                            {
                                ReflectionController.SetPropertyValue(variableWithExec.ActiveVariable, nameof(variableWithExec.ActiveVariable.WriteValue), parameters[0]);
                                //variableWithExec.ActiveVariable.WriteValue = parameters[0];
                            }
                            variableWithExec.ActiveVariable.Write();
                            variableWithExec.ExecuteVariable.Write();
                        }
                    };
                });
            }
        }

        private void SetCommands()
        {
            CommanderContainer.Objects.ToList().ForEach(x => x.Do(y => SetCommand(y)));
        }
    }
}

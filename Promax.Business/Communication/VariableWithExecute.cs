using RemoteVariableHandler.Core;
using System;
using System.ComponentModel;

namespace Promax.Business
{
    public class VariableWithExecute : IRemoteVariable
    {
        public VariableWithExecute(IRemoteVariable activeVariable, IRemoteVariable executeVariable)
        {
            ActiveVariable = activeVariable;
            ExecuteVariable = executeVariable;
        }

        public IRemoteVariable ActiveVariable { get; private set; }
        public IRemoteVariable ExecuteVariable { get; private set; }

        public string VariableName { get => ActiveVariable.VariableName; set => ActiveVariable.VariableName = value; }
        public object WriteValue { get => ActiveVariable.WriteValue; set => ActiveVariable.WriteValue = value; }
        public object ReadValue { get => ActiveVariable.ReadValue; set => ActiveVariable.ReadValue = value; }

        public Type Type => ActiveVariable.Type;

        public IVariableDefinition Definition { get => ActiveVariable.Definition; set => ActiveVariable.Definition = value; }
        public IVariableCommunicator Communicator { get => ActiveVariable.Communicator; set => ActiveVariable.Communicator = value; }

        public event EventHandler ReadValueChanged
        {
            add
            {
                ActiveVariable.ReadValueChanged += value;
            }

            remove
            {
                ActiveVariable.ReadValueChanged -= value;
            }
        }

        public event EventHandler WriteValueChanged
        {
            add
            {
                ActiveVariable.WriteValueChanged += value;
            }

            remove
            {
                ActiveVariable.WriteValueChanged -= value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                ActiveVariable.PropertyChanged += value;
            }

            remove
            {
                ActiveVariable.PropertyChanged -= value;
            }
        }
    }
}

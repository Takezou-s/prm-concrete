using Extensions;
using Promax.Core;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Utility.Binding;

namespace Promax.Business
{
    public class VariableController : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private bool _connected;
        private MyBinding _internalBindings = new MyBinding();
        private EasyModbusCommunicator VariableCommunicator;
        private VariablesBase VariableScope;
        private RemoteVariableExceptionHandler RemoteVariableExceptionHandler;
        private MyBinding VariableBindings;
        private List<BindingHandler> _containerBindingHandlers = new List<BindingHandler>();

        public VariableOwnerContainer VariableOwnerContainer { get; private set; }
        public string Ip { get; set; }
        public int Timeout { get; set; }
        public bool Connected
        {
            get
            {
                return _connected;
            }
            set
            {
                bool changed = false;
                if (!_connected.IsEqual(value))
                    changed = true;
                _connected = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Connected));
                }
            }
        }
        public VariableController(EasyModbusCommunicator variableCommunicator, VariablesBase variableScope, VariableOwnerContainer variableOwnerContainer, RemoteVariableExceptionHandler remoteVariableExceptionHandler, BackgroundProcessor backgroundProcessor)
        {
            VariableCommunicator = variableCommunicator;
            VariableScope = variableScope;
            RemoteVariableExceptionHandler = remoteVariableExceptionHandler;
            VariableBindings = new MyBinding(MyBindingType.Manual);
            VariableOwnerContainer = variableOwnerContainer;
            backgroundProcessor.RunAction = () => BackgroundProcess();
            backgroundProcessor.CompletedAction = () => BackgroundProcessDone();
            VariableOwnerContainer.ObjectRegistered += VariableOwnerContainer_ObjectRegistered;
            BindObjectsInContainer();
        }

        private void VariableOwnerContainer_ObjectRegistered(object container, IVariableOwner registeredObject)
        {
            registeredObject.BindDefault(VariableScope, VariableBindings);
        }

        public void Bind(IVariableOwner variableOwner)
        {
            variableOwner.BindDefault(VariableScope, VariableBindings);
        }
        private void BackgroundProcess()
        {
            if (!VariableCommunicator.Connected)
            {
                VariableCommunicator.Connect(Ip, Timeout * 100);
            }
            else
            {
                VariableCommunicator.ReadScope();
                for (int i = 0; i < VariableScope.RemoteVariables.Count; i++)
                {
                    VariableCommunicator.Read(VariableScope.RemoteVariables[i]);
                }
                KeyValuePair<IRemoteVariable, Action>[] actions = null;
                RemoteVariableExceptionHandler.Do(x => x.PendingActions.DoIf(y => y.Count > 0, y => actions = y.ToArray()));
                actions.Do(x =>
                {
                    foreach (var keyValuePair in x)
                    {
                        var variable = keyValuePair.Key;
                        var action = keyValuePair.Value;
                        action?.Invoke();
                        RemoteVariableExceptionHandler.Do(plant => plant.PendingActions.DoIf(pendingActions => variable != null && plant.Contains(variable), pendingActions => plant.Remove(variable)));
                    }
                });
            }
        }
        private void BackgroundProcessDone()
        {
            VariableBindings.Do(x => x.Map());
            VariableCommunicator.Do(x => Connected = VariableCommunicator.Connected);
        }

        private void BindObjectsInContainer()
        {
            _containerBindingHandlers.ForEach(x => x.Do(y => VariableBindings.RemoveBinding(y)));
            _containerBindingHandlers.Clear();
            VariableOwnerContainer.Objects.ToList().ForEach(x => x.BindDefault(VariableScope, VariableBindings).Do(y => _containerBindingHandlers.Add(y)));
        }
    }
}

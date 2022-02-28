using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    public abstract class VariableCommunicator : IVariableCommunicator
    {
        private static volatile object _locker = new object();
        /// <summary>
        /// Değişken tanımlarının hangi haberleşme objesiyle kullanılacağı bilgisini tutan dictionary.
        /// Type: IVariableDefinition interface'ini implement eden obje tipi.
        /// Type: Değişkenin tipi.
        /// IVariableCommunicator: IVariableDefinition'a uygun haberleşmeyi yapabilen obje.
        /// </summary>
        private Dictionary<Type, Dictionary<Type, IVariableCommunicator>> _communicatorsForDefinitions = new Dictionary<Type, Dictionary<Type, IVariableCommunicator>>();

        IVariableCommunicator GetCommunicatorForVariable(IRemoteVariable variable)
        {
            IVariableCommunicator communicator = null;
            bool notValid = false;
            if (!_communicatorsForDefinitions.ContainsKey(variable.Definition.GetType()))
            {
                notValid = true;
            }
            else if(!_communicatorsForDefinitions[variable.Definition.GetType()].ContainsKey(variable.Type))
            {
                notValid = true;
            }

            if(notValid)
                throw new InvalidCastException("Bu tanımlamaya sahip değişken için bir haberleşme objesi belirtilmemiş!");
            communicator = _communicatorsForDefinitions[variable.Definition.GetType()][variable.Type];
            return communicator;
        }

        public void Read(IRemoteVariable variable)
        {
            lock (_locker)
            {
                IVariableCommunicator communicator = GetCommunicatorForVariable(variable);
                communicator.Read(variable); 
            }
        }

        public async Task ReadAsync(IRemoteVariable variable)
        {
                IVariableCommunicator communicator = GetCommunicatorForVariable(variable);
                await communicator.ReadAsync(variable); 
            
        }

        public void Write(IRemoteVariable variable)
        {
            lock (_locker)
            {
                IVariableCommunicator communicator = GetCommunicatorForVariable(variable);
                communicator.Write(variable); 
            }
        }

        public async Task WriteAsync(IRemoteVariable variable)
        {
                IVariableCommunicator communicator = GetCommunicatorForVariable(variable);
                await communicator.WriteAsync(variable);
            
        }

        public void SetCommunicatorForType(Type variableDefinitionType, Type variableType, IVariableCommunicator communicator)
        {
            if (!_communicatorsForDefinitions.ContainsKey(variableDefinitionType))
            {
                _communicatorsForDefinitions.Add(variableDefinitionType, new Dictionary<Type, IVariableCommunicator>());
            }
            if (_communicatorsForDefinitions[variableDefinitionType].ContainsKey(variableType))
            {
                _communicatorsForDefinitions[variableDefinitionType].Remove(variableType);
            }
            _communicatorsForDefinitions[variableDefinitionType].Add(variableType, communicator);
        }
    }
}

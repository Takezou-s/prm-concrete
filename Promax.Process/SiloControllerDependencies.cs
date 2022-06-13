using Promax.Core;
using Promax.Entities;
using System.Collections.Generic;

namespace Promax.Process
{
    public class SiloControllerDependencies
    {
        public ParameterOwnerBase ParameterOwner { get; set; }
        public string NameInRecipeScope { get; set; }
        public string NameInParameterScope { get; set; }
        public string VariableOwnerName { get; set; }
        public string CommanderName { get; set; }
        public IVariables ParameterScope { get; set; }
        public IVariables RecipeScope { get; set; }
    }
    public class SiloInitializer
    {
        private Dictionary<string, SiloControllerDependencies> _siloControllerDependencies = new Dictionary<string, SiloControllerDependencies>();
        public Container<Silo> SiloContainer { get; set; }
        public void Add(string uniqueName, SiloControllerDependencies siloControllerDependencies)
        {
            Remove(uniqueName);
            _siloControllerDependencies.Add(uniqueName, siloControllerDependencies);
        }
        public void Remove(string uniqueName)
        {
            if (_siloControllerDependencies.ContainsKey(uniqueName))
                _siloControllerDependencies.Remove(uniqueName);
        }
        public SiloControllerDependencies GetSiloControllerDependencies(string uniqueName)
        {
            return _siloControllerDependencies[uniqueName];
        }
    }
}

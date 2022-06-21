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
        public int SiloNo { get; set; }
    }
    public class SiloControllerDependenciesBuilder
    {
        private ParameterOwnerBase parameterOwner;
        private string nameInRecipeScope;
        private string nameInParameterScope;
        private string variableOwnerName;
        private string commanderName;
        private IVariables parameterScope;
        private IVariables recipeScope;
        private int siloNo;

        public SiloControllerDependenciesBuilder SetParameterOwner(ParameterOwnerBase value)
        {
            parameterOwner = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetNameInRecipeScope(string value)
        {
            nameInRecipeScope = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetNameInParameterScope(string value)
        {
            nameInParameterScope = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetVariableOwnerName(string value)
        {
            variableOwnerName = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetCommanderName(string value)
        {
            commanderName = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetParameterScope(IVariables value)
        {
            parameterScope = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetRecipeScope(IVariables value)
        {
            recipeScope = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetSiloNo(int value)
        {
            siloNo = value;
            return this;
        }
        public SiloControllerDependenciesBuilder SetAllName(string value)
        {
            commanderName = value;
            variableOwnerName = value;
            nameInParameterScope = value;
            nameInRecipeScope = value;
            return this;
        }
        public SiloControllerDependencies Create()
        {
            return new SiloControllerDependencies() { ParameterOwner = parameterOwner, NameInRecipeScope = nameInRecipeScope, NameInParameterScope = nameInParameterScope, VariableOwnerName = variableOwnerName, CommanderName = commanderName, ParameterScope = parameterScope, RecipeScope = recipeScope };
        }
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

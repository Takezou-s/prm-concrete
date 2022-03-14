using Extensions;
using Promax.Core;
using RemoteVariableHandler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Promax.Business
{
    public abstract class VariablesBase : IVariables
    {
        protected List<PropertyInfo> Properties = new List<PropertyInfo>();
        protected List<object> Objects = new List<object>();
        protected IVariableCommunicator Communicator { get; set; }
        public abstract string ScopeName { get; set; }
        public virtual List<IRemoteVariable> RemoteVariables { get; private set; } = new List<IRemoteVariable>();
        protected VariablesBase(IVariableCommunicator communicator)
        {
            Communicator = communicator;
            InitVariables();
            Properties.AddRange(this.GetType().GetProperties());
            Properties.ForEach(propertyInfo => propertyInfo.TryCatch(propertyInfo1 => propertyInfo1.GetValue(this).DoReturn(propertyValue => Objects.Add(propertyValue)).DoIf(propertyValue => propertyValue is IRemoteVariable,
                propertyValue => (propertyValue as IRemoteVariable).DoReturn(remoteVariable => remoteVariable.VariableName = ScopeName + "." + propertyInfo.Name).Do(remoteVariable => RemoteVariables.Add(remoteVariable))), null));
            RemoteVariables.ForEach(x => x.Do(y => y.Communicator = Communicator));
        }
        public virtual bool IsObjectIncluded(object obj)
        {
            bool result = false;
            obj.DoIf(x => Objects.Contains(x), x => result = true);
            return result;
        }
        protected abstract void InitVariables();
        public IRemoteValue GetValue(string objectName, string propertyName)
        {
            IRemoteValue result = null;
            result = GetVariablePropertyInfo(objectName, propertyName)?.GetValue(this) as IRemoteValue;
            return result;
        }
        public T GetAs<T>(string objectName, string propertyName) where T : class
        {
            T result = default(T);
            result = result = GetVariablePropertyInfo(objectName, propertyName)?.GetValue(this) as T;
            return result;
        }
        public IRemoteVariable GetVariable(string objectName, string propertyName)
        {
            IRemoteVariable result = null;
            result = GetVariablePropertyInfo(objectName, propertyName)?.GetValue(this) as IRemoteVariable;
            return result;
        }
        public IRemoteVariable GetVariable(string objectName, string propertyName, Type attribute)
        {
            IRemoteVariable result = null;
            GetVariablePropertyInfo(objectName, propertyName).Do(propInfo =>
            {
                propInfo.GetCustomAttribute(attribute, true).Do(attr => result = GetVariable(objectName, propertyName));
            });
            return result;
        }
        public PropertyInfo GetVariablePropertyInfo(string objectName, string propertyName)
        {
            PropertyInfo result = null;
            result = Properties.FirstOrDefault(x => x.Name == objectName + "_" + propertyName);
            return result;
        }
    }
}

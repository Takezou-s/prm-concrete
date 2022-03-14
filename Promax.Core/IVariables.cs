using RemoteVariableHandler.Core;
using System;
using System.Reflection;

namespace Promax.Core
{
    public interface IVariables
    {
        IRemoteValue GetValue(string objectName, string propertyName);
        T GetAs<T>(string objectName, string propertyName) where T : class ; 
        IRemoteVariable GetVariable(string objectName, string propertyName);
        IRemoteVariable GetVariable(string objectName, string propertyName, Type attribute);
        PropertyInfo GetVariablePropertyInfo(string objectName, string propertyName);
    }
}

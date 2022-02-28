using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Promax.Core
{
    public abstract class ParameterOwnerContext
    {
        [JsonIgnore]
        public List<ParameterOwnerBase> ParameterOwners { get; set; } = new List<ParameterOwnerBase>();
        public ParameterOwnerContext()
        {
            InitParameterOwners();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                object propValue = property.GetValue(this);
                if (propValue != null)
                {
                    if (propValue is ParameterOwnerBase)
                    {
                        ParameterOwners.Add(propValue as ParameterOwnerBase);
                    }
                }
            }
        }
        public abstract void InitParameterOwners();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC
{
    public class VirtualPLCObject
    {
    }
    public class VirtualPLCProperty
    {

        public static VirtualPLCProperty Register(string name, Type propertyType, Type ownerType)
        {
            var result = new VirtualPLCProperty();
            result.Name = name;
            result.PropertyType = propertyType;
            result.OwnerType = ownerType;
        }

        public static VirtualPLCProperty Register(string name, Type propertyType, Type ownerType, object defaultValue)
        {
            var result = new VirtualPLCProperty();
            result.Name = name;
            result.PropertyType = propertyType;
            result.OwnerType = ownerType;
            result.Value = defaultValue == null ? GetDefaultOfType(propertyType) : Convert.ChangeType(defaultValue, propertyType);
            return result;
        }

        private static object GetDefaultOfType(Type propertyType)
        {
            object result = null;
            var methodInfo = typeof(VirtualPLCProperty).GetMethod("GetDefault");
            if (methodInfo != null)
            {
                methodInfo.MakeGenericMethod(propertyType);
                result = methodInfo.Invoke(null, null);
            }
            return result;
        }
        private static T GetDefault<T>()
        {
            return default(T);
        }

        private VirtualPLCProperty()
        {

        }

        private object Value { get; set; }
        public string Name { get; private set; }
        public Type PropertyType { get; private set; }
        public Type OwnerType { get; private set; }
    }
}

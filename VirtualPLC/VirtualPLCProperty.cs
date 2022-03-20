using Extensions;
using System;
using System.Reflection;

namespace VirtualPLC
{
    public class VirtualPLCProperty
    {
        private object _value;
        private bool _changed;

        public static VirtualPLCProperty Register(string name, Type propertyType, IVirtualPLCPropertyOwner owner, bool input, bool retain, bool output)
        {
            return Register(name, propertyType, owner, input, retain, output, null);
        }

        public static VirtualPLCProperty Register(string name, Type propertyType, IVirtualPLCPropertyOwner owner, bool input, bool retain, bool output, object defaultValue)
        {
            var result = new VirtualPLCProperty();
            result.Name = name;
            result.PropertyType = propertyType;
            result.Owner = owner;
            result.Input = input;
            result.Retain = retain;
            result.Value = defaultValue == null ? GetDefaultOfType(propertyType) : Convert.ChangeType(defaultValue, propertyType);
            result.TemporaryValue = result.Value;
            owner.VirtualPLCProperties.Add(result);
            return result;
        }

        #region GetDefault
        private static object GetDefaultOfType(Type propertyType)
        {
            object result = null;
            var methodInfo = typeof(VirtualPLCProperty).GetMethod("GetDefault", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            if (methodInfo != null)
            {
                var genericMethodInfo = methodInfo.MakeGenericMethod(propertyType);
                result = genericMethodInfo.Invoke(null, null);
            }
            return result;
        }
        private static T GetDefault<T>()
        {
            return default(T);
        } 
        #endregion

        private VirtualPLCProperty()
        {

        }
        
        private object Value
        {
            get
            {
                return _value;
            }
            set
            {
                bool changed = false;
                if (!_value.IsEqual(value))
                    changed = true;
                _value = Convert.ChangeType(value, PropertyType);
                if (changed)
                {
                    if (!Output)
                    {
                        OnPropertyChanged(); 
                    }
                    else
                    {
                        _changed = true;
                    }
                }
            }
        }
        private object TemporaryValue { get; set; }

        public string Name { get; private set; }
        public Type PropertyType { get; private set; }
        public IVirtualPLCPropertyOwner Owner { get; private set; }
        public bool Input { get; set; }
        public bool Output { get; set; }
        public bool Retain { get; set; }

        public void SetValue(object value)
        {
            if(Input)
                TemporaryValue = Convert.ChangeType(value, PropertyType);
            else
                Value = Convert.ChangeType(value, PropertyType);
        }

        public object GetValue()
        {
            return Value;
        }

        public void RestoreValue()
        {
            if (!Input)
                return;
            Value = TemporaryValue;
        }

        public void SetOutputValue()
        {
            if(Output && _changed)
            {
                OnPropertyChanged();
                _changed = false;
            }
        }

        private void OnPropertyChanged()
        {
            Owner?.OnPropertyChanged(Name);
        }
    }
}

using System;
using System.ComponentModel;
using System.Reflection;
using System.Globalization;

namespace Utility.Binding
{
    public static class ReflectionController
    {
        public static void SetPropertyValue(object Instance, string PropertyName, object Value)
        {
            if (PropertyName.Contains("."))
            {
                string[] split = PropertyName.Split('.');
                string newProp = string.Empty;
                for (int i = 1; i <= split.Length - 1; i++)
                {
                    newProp += split[i] + ".";
                }
                newProp = newProp.Remove(newProp.Length - 1, 1);
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object newobj = null;
                if (prp != null)
                    newobj = prp.GetValue(Instance);
                else if (prp1 != null)
                    newobj = prp1.GetValue(Instance);
                if (newobj != null)
                    SetPropertyValue(newobj, newProp, Value);
            }
            else
            {
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (prp != null)
                    prp.SetValue(Instance, Convert.ChangeType(Value, prp.PropertyType));
                else if (prp1 != null)
                    prp1.SetValue(Instance, Convert.ChangeType(Value, prp.PropertyType));
            }
        }
        public static void SetPropertyValueInvariantCulture(object Instance, string PropertyName, object Value)
        {
            if (PropertyName.Contains("."))
            {
                string[] split = PropertyName.Split('.');
                string newProp = string.Empty;
                for (int i = 1; i <= split.Length - 1; i++)
                {
                    newProp += split[i] + ".";
                }
                newProp = newProp.Remove(newProp.Length - 1, 1);
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object newobj = null;
                if (prp != null)
                    newobj = prp.GetValue(Instance);
                else if (prp1 != null)
                    newobj = prp1.GetValue(Instance);
                if (newobj != null)
                    SetPropertyValueInvariantCulture(newobj, newProp, Value);
            }
            else
            {
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (prp != null)
                    prp.SetValue(Instance, Convert.ChangeType(Value, prp.PropertyType, CultureInfo.InvariantCulture));
                else if (prp1 != null)
                    prp1.SetValue(Instance, Convert.ChangeType(Value, prp1.FieldType, CultureInfo.InvariantCulture));
            }
        }
        public static void SetPropertyValueInvariantCultureConverted(object Instance, string PropertyName, object Value)
        {
            if (PropertyName.Contains("."))
            {
                string[] split = PropertyName.Split('.');
                string newProp = string.Empty;
                for (int i = 1; i <= split.Length - 1; i++)
                {
                    newProp += split[i] + ".";
                }
                newProp = newProp.Remove(newProp.Length - 1, 1);
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object newobj = null;
                if (prp != null)
                    newobj = prp.GetValue(Instance);
                else if (prp1 != null)
                    newobj = prp1.GetValue(Instance);
                if (newobj != null)
                    SetPropertyValueInvariantCultureConverted(newobj, newProp, Value);
            }
            else
            {
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (prp != null)
                {
                    var converter = TypeDescriptor.GetConverter(prp.PropertyType);
                    prp.SetValue(Instance, converter.ConvertFromInvariantString((string)Value));
                }
                else if (prp1 != null)
                {
                    var converter = TypeDescriptor.GetConverter(prp1.FieldType);
                    prp1.SetValue(Instance, converter.ConvertFromInvariantString((string)Value));
                }
            }
        }
        public static object GetPropertyValue(object Instance, string PropertyName)
        {
            try
            {
                object Value = null;
                if (PropertyName.Contains("."))
                {
                    string[] split = PropertyName.Split('.');
                    string newProp = string.Empty;
                    for (int i = 1; i <= split.Length - 1; i++)
                    {
                        newProp += split[i] + ".";
                    }
                    newProp = newProp.Remove(newProp.Length - 1, 1);
                    Type type = Instance.GetType();
                    PropertyInfo prp = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    FieldInfo prp1 = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    object newobj = null;
                    if (prp != null)
                        newobj = prp.GetValue(Instance);
                    else if (prp1 != null)
                        newobj = prp1.GetValue(Instance);
                    if (newobj != null)
                        Value = GetPropertyValue(newobj, newProp);
                }
                else
                {
                    Type type = Instance.GetType();
                    PropertyInfo prp = type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    FieldInfo prp1 = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (prp != null)
                        Value = prp.GetValue(Instance);
                    else if (prp1 != null)
                        Value = prp1.GetValue(Instance);
                }
                return Value;
            }
            catch (Exception exp2)
            {
                return null;
            }
        }
        public static object GetPropertyValueConverted(object Instance, string PropertyName)
        {
            object Value = null;
            if (PropertyName.Contains("."))
            {
                string[] split = PropertyName.Split('.');
                string newProp = string.Empty;
                for (int i = 1; i <= split.Length - 1; i++)
                {
                    newProp += split[i] + ".";
                }
                newProp = newProp.Remove(newProp.Length - 1, 1);
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo prp1 = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object newobj = null;
                if (prp != null)
                    newobj = prp.GetValue(Instance);
                else if (prp1 != null)
                    newobj = prp1.GetValue(Instance);
                if (newobj != null)
                    Value = GetPropertyValueConverted(newobj, newProp);
            }
            else
            {
                Type type = Instance.GetType();
                PropertyInfo prp = type.GetProperty(PropertyName);
                FieldInfo prp1 = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (prp != null && prp.PropertyType != null)
                {
                    var converter = TypeDescriptor.GetConverter(prp.PropertyType);
                    Value = converter.ConvertToInvariantString(prp.GetValue(Instance));

                }
                else if (prp1 != null && prp1.FieldType != null)
                {
                    var converter = TypeDescriptor.GetConverter(prp1.FieldType);
                    Value = converter.ConvertToInvariantString(prp1.GetValue(Instance));
                }
            }
            return Value;
        }
        public static object GetProperty(object Instance, string PropertyName)
        {
            object value = null;
            if (PropertyName.Contains("."))
            {
                string[] split = PropertyName.Split('.');
                string newProperty = string.Empty;
                for (int i = 1; i <= split.Length - 1; i++)
                {
                    newProperty += split[i] + ".";
                }
                newProperty = newProperty.Remove(newProperty.Length - 1, 1);
                Type type = Instance.GetType();
                PropertyInfo property = type.GetProperty(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo field = type.GetField(split[0], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                object newInstance = null;
                if (property != null)
                    newInstance = property.GetValue(Instance);
                else if (field != null)
                    newInstance = field.GetValue(Instance);
                if (newInstance != null)
                    value = GetProperty(newInstance, newProperty);
            }
            else
            {
                Type type = Instance.GetType();
                PropertyInfo property = type.GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                FieldInfo field = type.GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                    value = property;
                else if (field != null)
                    value = field;
            }
            return value;
        }
    }
}

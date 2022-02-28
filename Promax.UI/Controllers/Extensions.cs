using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Promax.UI
{
    static class Extensions
    {
        public static List<T> MergeResults<T>(this IEnumerable<T> list, string property, Action<T> addingDictionaryAction, Action<T, T> iteratingAction)
            where T : class, ICloneable, new()
        {
            var propInfo = typeof(T).GetProperty(property);
            if (propInfo == null)
                return null;
            Type propertyType = propInfo.PropertyType;
            Type dicType = typeof(Dictionary<,>);
            Type[] typeArguments = { propertyType, typeof(T) };
            Type dicTypeGeneric = dicType.MakeGenericType(typeArguments);
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(dicTypeGeneric);

            foreach (var item in list)
            {
                object key = propInfo.GetValue(item);
                if (!dictionary.Contains(key))
                {
                    T obj = item.Clone() as T;
                    dictionary.Add(key, obj);
                    addingDictionaryAction(obj);
                }
                iteratingAction(dictionary[key] as T, item);
            }
            List<T> listu = new List<T>();
            foreach (var item in dictionary.Values)
            {
                listu.Add(item as T);
            }
            return listu;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteVariableHandler.Core
{
    internal static class Extensions
    {
        /// <summary>
        /// Automated null check.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsEqual(this object o, object obj)
        {
            bool result = false;
            if (o == null && obj == null)
            {
                result = true;
            }
            else if ((o == null && obj != null) || (o != null && obj == null))
            {
                result = false;
            }
            else
            {
                o.DoIf(x => x.Equals(obj), x => result = true);
            }
            return result;
        }
        internal static void Do<T>(this T obj, Action<T> action)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
        }
        internal static void DoIf<T>(this T obj, Func<T, bool> predicate, Action<T> action)
        {
            if (obj != null && action != null && predicate(obj))
            {
                action(obj);
            }
        }
        internal static void DoIfElse<T>(this T obj, Func<T, bool> predicate, Action<T> action, Action<T> elseAction)
        {
            if (obj != null && action != null)
            {
                if (predicate(obj))
                {
                    obj.Do(action);
                }
                else
                {
                    obj.Do(elseAction);
                }
            }
        }
        internal static T DoIfReturn<T>(this T obj, Func<T, bool> predicate, Action<T> action)
        {
            if (obj != null && action != null && predicate(obj))
            {
                action(obj);
            }
            return obj;
        }
        internal static T DoReturn<T>(this T obj, Action<T> action)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
            return obj;
        }
        internal static T DoReturn<T>(this T obj, Action<T> action, Action action2)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
            else
            {
                action2();
            }
            return obj;
        }
        internal static void Do<T>(this T obj, Action<T> action, Action action2)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
            else
            {
                action2();
            }
        }
    }
}

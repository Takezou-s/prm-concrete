using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static string ToTitleCase(this string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
        public static string RemoveAfter(this string str, string character)
        {
            string result = str;
            if(!string.IsNullOrEmpty(str))
            {
                int index = str.IndexOf(character);
                if (index >= 0)
                {
                    result=str.Remove(index);
                }
            }
            return result;
        }
        /// <summary>
        /// Automated null check.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEqual(this object o, object obj)
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
        /// <summary>
        /// Firebird veritabanında boolean olarak bulunan sütunlarda dönüşüm problemli olduğu için bu ifadeler string olarak belirlendi.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Boolify(this string value)
        {
            return value.ToLower() != "false" && value.ToLower() != "true" ? "false" : value.ToLower();
        }
        public static DateTime MakeFirstDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }
        public static DateTime MakeLastDate(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            DateTime result = dt;
            int diff = (7 + (result.DayOfWeek - startOfWeek)) % 7;
            return result.AddDays(-1 * diff).Date;
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            DateTime result = dt;
            return result.StartOfWeek(startOfWeek).AddDays(6);
        }
        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1, dt.Hour, dt.Minute, dt.Second);
        }
        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31, dt.Hour, dt.Minute, dt.Second);
        }
        public static bool IsEarlierThan(this DateTime dt, DateTime dt2)
        {
            return DateTime.Compare(dt, dt2) < 0;
        }
        public static bool IsLaterThan(this DateTime dt, DateTime dt2)
        {
            return DateTime.Compare(dt, dt2) > 0;
        }
        public static bool IsSame(this DateTime dt, DateTime dt2)
        {
            return DateTime.Compare(dt, dt2) == 0;
        }
        public static double Remaining(this double DesiredQuantity, double EjectedQuantity)
        {
            return DesiredQuantity - EjectedQuantity;
        }
        public static double Excess(this double DesiredQuantity, double EjectedQuantity)
        {
            return -DesiredQuantity.Remaining(EjectedQuantity);
        }
        public static void Try<T>(this T obj, Action<T> @try)
        {
            try
            {
                obj.Do(x => @try(x));
            }
            catch
            {
                throw;
            }
        }
        public static Exception TryCatch<T>(this T obj, Action<T> @try, Action @catch)
        {
            Exception exception = null;
            try
            {
                obj.Try(x => @try(x));
            }
            catch (Exception exp)
            {
                exception = exp;
                @catch.Do(x => x.Invoke());
            }
            return exception;
        }
        public static Exception TryCatchFinally<T>(T obj, Action<T> @try, Action @catch, Action @finally)
        {
            Exception exception = null;
            exception = obj.TryCatch(x => @try(x), @catch);
            @finally();
            return exception;
        }
        public static void Do<T>(this T obj, Action<T> action)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
        }
        public static void DoIf<T>(this T obj, Func<T, bool> predicate, Action<T> action)
        {
            if (obj != null && action != null && predicate(obj))
            {
                action(obj);
            }
        }
        public static void DoIfElse<T>(this T obj, Func<T, bool> predicate, Action<T> action, Action<T> elseAction)
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
        public static T DoIfReturn<T>(this T obj, Func<T, bool> predicate, Action<T> action)
        {
            if (obj != null && action != null && predicate(obj))
            {
                action(obj);
            }
            return obj;
        }
        public static T DoReturn<T>(this T obj, Action<T> action)
        {
            if (obj != null && action != null)
            {
                action(obj);
            }
            return obj;
        }
        public static T DoReturn<T>(this T obj, Action<T> action, Action action2)
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
        public static void Do<T>(this T obj, Action<T> action, Action action2)
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Promax.UI
{
    public static class Strings
    {
        private static Dictionary<string, string> _propValuePairs = new Dictionary<string, string>();
        private static Dictionary<string, string> _defaultValuePropPairs = new Dictionary<string, string>();
        static Strings()
        {
            PropertyInfo[] properties = typeof(Strings).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(null);
                    if (!_propValuePairs.ContainsKey(property.Name))
                    {
                        _propValuePairs.Add(property.Name, value);
                    }
                    if (!_defaultValuePropPairs.ContainsKey(value))
                    {
                        _defaultValuePropPairs.Add(value, property.Name);
                    }
                }
            }

        }
        static string Get(string name)
        {
            PropertyInfo prop = typeof(Strings).GetProperty(name);
            if (prop != null)
            {
                return (string)prop.GetValue(null);
            }
            return string.Empty;
        }
        public static string GetByDefaultValue(string defaultValue)
        {
            string result = defaultValue;
            if (_defaultValuePropPairs.ContainsKey(defaultValue))
            {
                var a = Get(_defaultValuePropPairs[defaultValue]);
                if (!string.IsNullOrEmpty(a))
                    result = a;
            }
            return result;
        }
        public static void Set(string name, string value)
        {
            PropertyInfo prop = typeof(Strings).GetProperty(name);
            if (prop != null)
            {
                prop.SetValue(null, value);
            }
        }
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void SaveChanges()
        {
            var a = _propValuePairs.ToArray();
            foreach (var item in a)
            {
                var prop = item.Key;
                var value = item.Value;
                var presentValue = Get(prop);
                if (presentValue != value)
                {
                    _propValuePairs[prop] = presentValue;
                    StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
                }
            }
        }

        public static string KurumsalMüşteri { get; set; } = "Kurumsal Müşteri";
        public static string BireyselMüşteri { get; set; } = "Bireysel Müşteri";
        public static string HafifBeton { get; set; } = "Hafif Beton";
        public static string NormalBeton { get; set; } = "Normal Beton";
        public static string AğırBeton { get; set; } = "Ağır Beton";
        public static string S1 { get; set; } = "S1";
        public static string S2 { get; set; } = "S2";
        public static string S3 { get; set; } = "S3";
        public static string S4 { get; set; } = "S4";
        public static string S5 { get; set; } = "S5";
        public static string X0 { get; set; } = "X0";
        public static string XC1 { get; set; } = "XC1";
        public static string XC2 { get; set; } = "XC2";
        public static string XC3 { get; set; } = "XC3";
        public static string XC4 { get; set; } = "XC4";
        public static string XD1 { get; set; } = "XD1";
        public static string XD2 { get; set; } = "XD2";
        public static string XD3 { get; set; } = "XD3";
        public static string XS1 { get; set; } = "XS1";
        public static string XS2 { get; set; } = "XS2";
        public static string XS3 { get; set; } = "XS3";
        public static string XF1 { get; set; } = "XF1";
        public static string XF2 { get; set; } = "XF2";
        public static string XF3 { get; set; } = "XF3";
        public static string XA1 { get; set; } = "XA1";
        public static string XA2 { get; set; } = "XA2";
        public static string XA3 { get; set; } = "XA3";
        public static string OnayBekleniyor { get; set; } = "Onay Bekleniyor";
        public static string Onaylandı { get; set; } = "Onaylandı";
        public static string Reddedildi { get; set; } = "Reddedildi";
    }
}

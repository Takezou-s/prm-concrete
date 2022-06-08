using Extensions;
using System;
using System.Reflection;

namespace VirtualPLC
{
    public class VirtualPLCPropertyBuilder
    {
        private string _name;
        private Type _type;
        private IVirtualPLCPropertyOwner _owner;
        bool input, retain, output;
        object defaultValue;

        public VirtualPLCPropertyBuilder(IVirtualPLCPropertyOwner owner)
        {
            _owner = owner;
        }

        public VirtualPLCPropertyBuilder()
        {
        }
        /// <summary>
        /// PropertyName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Name(string name)
        {
            _name = name;
            return this;
        }
        /// <summary>
        /// PropertyType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Type(Type type)
        {
            _type = type;
            return this;
        }
        /// <summary>
        /// Owner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Owner(IVirtualPLCPropertyOwner owner)
        {
            _owner = owner;
            return this;
        }
        /// <summary>
        /// True iken SetValue methodu girilen değeri TemporaryValue değişkeninde saklar, değerin Value değişkenine atılması için RestoreValue methodu çağırılmalıdır.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Input(bool value)
        {
            input = value;
            return this;
        }
        /// <summary>
        /// True iken Value değerinin değişimi dışarı bildirilmez, bildirilmesi için SetOutputValue methodu çağırılmalıdır.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Output(bool value)
        {
            output = value;
            return this;
        }
        /// <summary>
        /// Değişkenin değerinin saklanıp saklanmayacağını belirtir.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Retain(bool value)
        {
            retain = value;
            return this;
        }
        public VirtualPLCPropertyBuilder Default(object value)
        {
            defaultValue = value;
            return this;
        }
        /// <summary>
        /// Owner hariç resetler.
        /// </summary>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder Reset()
        {
            _name = string.Empty;
            _type = null;
            input = false;
            retain = false;
            output = false;
            defaultValue = null;
            return this;
        }
        /// <summary>
        /// Owner dahil resetler.
        /// </summary>
        /// <returns></returns>
        public VirtualPLCPropertyBuilder FullReset()
        {
            _owner = null;
            Reset();
            return this;
        }
        public VirtualPLCProperty Get()
        {
            return VirtualPLCProperty.Register(_name, _type, _owner, input, retain, output, defaultValue);
        }

    }
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
        /// <summary>
        /// Belirtilen tipin default değerini geri döndürür.
        /// </summary>
        /// <param name="propertyType">Tip</param>
        /// <returns></returns>
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
        /// <summary>
        /// Belirtilen generic tipin default değerini geri döndürür.
        /// </summary>
        /// <typeparam name="T">Generic tip</typeparam>
        /// <returns></returns>
        private static T GetDefault<T>()
        {
            return default(T);
        }
        #endregion

        private VirtualPLCProperty()
        {

        }
        /// <summary>
        /// Saklanan değer. Eğer VirtualPLCProperty Output olarak belirlenmişse değişiklik bilgisi dışarı verilmez.
        /// </summary>
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
        /// <summary>
        /// Geçici değer. 
        /// </summary>
        private object TemporaryValue { get; set; }
        /// <summary>
        /// VirtualPLCProperty'nin ismi.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// VirtualPLCProperty'nin tipi.
        /// </summary>
        public Type PropertyType { get; private set; }
        /// <summary>
        /// VirtualPLCProperty'nin ait olduğu container.
        /// </summary>
        public IVirtualPLCPropertyOwner Owner { get; private set; }
        /// <summary>
        /// True iken SetValue methodu girilen değeri TemporaryValue değişkeninde saklar, değerin Value değişkenine atılması için RestoreValue methodu çağırılmalıdır.
        /// </summary>
        public bool Input { get; set; }
        /// <summary>
        /// True iken Value değerinin değişimi dışarı bildirilmez, bildirilmesi için SetOutputValue methodu çağırılmalıdır.
        /// </summary>
        public bool Output { get; set; }
        /// <summary>
        /// Değişkenin değerinin saklanıp saklanmayacağını belirtir.
        /// </summary>
        public bool Retain { get; set; }
        /// <summary>
        /// Input değişkeninin durumuna göre Value veya TemporaryValue değişkenine girilen değeri yazar.
        /// </summary>
        /// <param name="value">Değer</param>
        public void SetValue(object value)
        {
            if (Input)
                TemporaryValue = Convert.ChangeType(value, PropertyType);
            else
                Value = Convert.ChangeType(value, PropertyType);
        }
        /// <summary>
        /// VirtualPLCProperty'nin değerini döndürür.
        /// </summary>
        /// <returns>VirtualPLCProperty Değeri</returns>
        public object GetValue()
        {
            return Value;
        }
        /// <summary>
        /// Geçici olarak saklanan değeri gerçek değere yazar.
        /// </summary>
        public void RestoreValue()
        {
            if (!Input)
                return;
            Value = TemporaryValue;
        }
        /// <summary>
        /// Output değişkeni True iken değerde bir değişim varsa bu değişimi dışarı bildirir.
        /// </summary>
        public void SetOutputValue()
        {
            if (Output && _changed)
            {
                OnPropertyChanged();
                _changed = false;
            }
        }
        /// <summary>
        /// Owner objenin OnPropertyChanged methodu aracılığıyla değer değişimini bildirir.
        /// </summary>
        private void OnPropertyChanged()
        {
            Owner?.OnPropertyChanged(Name);
        }
    }
}

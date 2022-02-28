using Extensions;
using RemoteVariableHandler.Core;
using System.ComponentModel;

namespace Promax.Core
{
    public class Parameter<T> : IParameter<T>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private string _code;
        private string _name;
        private T _value;
        private string _unit;
        #endregion
        #region PropertiesByAutoPropCreator
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                bool changed = false;
                if (!_code.IsEqual(value))
                    changed = true;
                _code = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Code));
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                bool changed = false;
                if (!_name.IsEqual(value))
                    changed = true;
                _name = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public T Value
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
                _value = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                bool changed = false;
                if (!_unit.IsEqual(value))
                    changed = true;
                _unit = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(Unit));
                }
            }
        }
        object IParameter.Value { get => Value; set => value.TryCatch(x => Value = (T)value, () => {; }); }
        #endregion
        public Parameter()
        {
        }
        public Parameter(string name, T value, string unit)
        {
            Name = name;
            Value = value;
            Unit = unit;
            Code = name.ToTitleCase().Replace(" ", string.Empty);
        }
        public Parameter(string name, T value, string unit, string code)
        {
            Name = name;
            Value = value;
            Unit = unit;
            Code = code;
        }
    }
}

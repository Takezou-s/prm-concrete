using Extensions;
using RemoteVariableHandler.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Promax.Core
{
    public abstract class ParameterOwnerBase : INameableTarget, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region FieldsByAutoPropCreator
        private string _name;
        private string _displayName;
        #endregion
        #region PropertiesByAutoPropCreator
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
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                bool changed = false;
                if (!_displayName.IsEqual(value))
                    changed = true;
                _displayName = value;
                if (changed)
                {
                    OnPropertyChanged(nameof(DisplayName));
                }
            }
        }
        #endregion

        public List<IParameter> Parameters { get; set; } = new List<IParameter>();
        public string NameableId { get => Name; set {; } }

        protected ParameterOwnerBase()
        {
            Init();
        }

        public virtual void Init()
        {
            InitParameters();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                object propValue = property.GetValue(this);
                if (propValue != null)
                {
                    if (propValue is IParameter)
                    {
                        Parameters.Add(propValue as IParameter);
                    }
                }
            }
        }

        protected ParameterOwnerBase(string name, string displayName) : this()
        {
            Name = name;
            DisplayName = displayName;
        }

        public abstract void InitParameters();
    }
}

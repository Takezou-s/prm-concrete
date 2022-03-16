using Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPLC
{
    public class VirtualPLCObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<VirtualPLCProperty> VirtualPLCProperties { get; } = new List<VirtualPLCProperty>();
        public VirtualController Controller { get; private set; }

        public VirtualPLCObject(VirtualController controller)
        {
            Controller = controller;
            Controller.VirtualPLCObjects.Add(this);
        }

        public void RestoreInputs()
        {
            VirtualPLCProperties.ForEach(x => x.RestoreValue());
        }
    }
    public class VirtualPLCProperty
    {
        private object _value;

        public static VirtualPLCProperty Register(string name, Type propertyType, VirtualPLCObject owner, bool input, bool retain)
        {
            return Register(name, propertyType, owner, input, retain, null);
        }

        public static VirtualPLCProperty Register(string name, Type propertyType, VirtualPLCObject owner, bool input, bool retain, object defaultValue)
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
                _value = value;
                if (changed)
                {
                    OnPropertyChanged();
                }
            }
        }
        private object TemporaryValue { get; set; }

        public string Name { get; private set; }
        public Type PropertyType { get; private set; }
        public VirtualPLCObject Owner { get; private set; }
        public bool Input { get; set; }
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

        private void OnPropertyChanged()
        {
            Owner?.OnPropertyChanged(Name);
        }
    }
    public class VirtualController
    {
        protected BackgroundWorker _worker;

        public List<VirtualPLCObject> VirtualPLCObjects { get; } = new List<VirtualPLCObject>();

        public VirtualController()
        {
            _worker = new BackgroundWorker();
            _worker.DoWork += Run;
            _worker.RunWorkerCompleted += RunCompleted;
            _worker.RunWorkerAsync();
        }

        private void Run(object sender, DoWorkEventArgs e)
        {
            RestoreInputs();
            Process();

        }

        private void RunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void RestoreInputs()
        {
            VirtualPLCObjects.ForEach(x => x.RestoreInputs());
        }

        protected virtual void Process()
        {

        }
    }
}

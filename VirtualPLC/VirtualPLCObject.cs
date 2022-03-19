using Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Binding;

namespace VirtualPLC
{
    public class VirtualPLCObject : IVirtualPLCPropertyOwner
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

        public void SetOutputs()
        {
            VirtualPLCProperties.ForEach(x => x.SetOutputValue());
        }
    }
    public interface IVirtualPLCPropertyOwner : INotifyPropertyChanged
    {
        List<VirtualPLCProperty> VirtualPLCProperties { get; }
        void OnPropertyChanged(string propertyName);
    }
    public class VirtualPLCProperty
    {
        private object _value;
        private bool _changed;

        public static VirtualPLCProperty Register(string name, Type propertyType, IVirtualPLCPropertyOwner owner, bool input, bool retain)
        {
            return Register(name, propertyType, owner, input, retain, null);
        }

        public static VirtualPLCProperty Register(string name, Type propertyType, IVirtualPLCPropertyOwner owner, bool input, bool retain, object defaultValue)
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
   
    public class VirtualController : IVirtualPLCPropertyOwner
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public List<VirtualPLCProperty> VirtualPLCProperties { get; } = new List<VirtualPLCProperty>();
        protected BackgroundWorker _worker;
        protected List<string> RetainPaths = new List<string>();

        public List<VirtualPLCObject> VirtualPLCObjects { get; } = new List<VirtualPLCObject>();
        public string Path { get; set; }

        public VirtualController(string path) : this(true, path)
        {
            
        }

        public VirtualController(bool init, string path)
        {
            Path = path;
            Init();
        }

        public void Init()
        {
            InitImp();
            FindRetainVariables();
            RestoreRetains();
            _worker = new BackgroundWorker();
            _worker.DoWork += Run;
            _worker.RunWorkerCompleted += RunCompleted;
            _worker.RunWorkerAsync();
        }

        protected virtual void InitImp()
        {
         
        }

        private void Run(object sender, DoWorkEventArgs e)
        {
            RestoreInputs();
            Process();
            SaveRetains();
        }

        private void RunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                SetOutputs();
            }
            catch
            {
                ;
            }
            finally
            {
                _worker.RunWorkerAsync(); 
            }
        }

        private void RestoreInputs()
        {
            VirtualPLCObjects.ForEach(x => x.RestoreInputs());
        }

        private void SetOutputs()
        {
            VirtualPLCObjects.ForEach(x => x.SetOutputs());
        }

        private void FindRetainVariables()
        {
            ListObjectsProperties(this, string.Empty);
        }

        private void ListObjectsProperties(object obj, string parentPath)
        {
            if(obj != null && !(obj is VirtualPLCProperty))
            {
                var properties = obj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var propValue = property.GetValue(obj);
                    if (propValue != null)
                    {
                        if (!(propValue is VirtualController))
                        {
                            if (propValue is VirtualPLCProperty && (propValue as VirtualPLCProperty).Retain)
                            {
                                string path = !string.IsNullOrEmpty(parentPath) && !string.IsNullOrWhiteSpace(parentPath) ? parentPath.Trim() + "." : string.Empty;
                                path += property.Name;
                                RetainPaths.Add(path);
                            }
                            else if (propValue is IVirtualPLCPropertyOwner)
                            {
                                string path = !string.IsNullOrEmpty(parentPath) && !string.IsNullOrWhiteSpace(parentPath) ? parentPath.Trim() + "." : string.Empty;
                                path += property.Name;
                                ListObjectsProperties(propValue, path);
                            } 
                        }
                    }
                }
            }
        }

        protected virtual void Process()
        {

        }

        protected virtual void SaveRetains()
        {
            List<string> datas = new List<string>();
            foreach (var item in RetainPaths)
            {
                try
                {
                    var value = ReflectionController.GetPropertyValue(this, item);
                    var data = new RetainSerializationData() { Path = item, Value = value };
                    datas.Add(JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
                }
                catch
                {
                    ;
                }
            }
            File.WriteAllLines(Path, datas);
        }

        protected virtual void RestoreRetains()
        {
            try
            {
                var datas = File.ReadAllLines(Path);
                foreach (var item in datas)
                {
                    try
                    {
                        var data = JsonConvert.DeserializeObject<RetainSerializationData>(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                        ReflectionController.SetPropertyValue(this, data.Path, data.Value);
                    }
                    catch
                    {
                        ;
                    }
                }
            }
            catch
            {
                ;
            }
        }
    }
    public class RetainSerializationData
    {
        public string Path { get; set; }
        public object Value { get; set; }
    }
}

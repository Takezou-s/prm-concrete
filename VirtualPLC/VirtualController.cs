using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Utility.Binding;

namespace VirtualPLC
{
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
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using Utility.Binding;

namespace VirtualPLC
{
    public class VirtualController : IVirtualPLCPropertyOwner
    {
        private volatile object _retainLocker = new object();
        /// <summary>
        /// Dinamik olarak oluşturulan objeler için kullanılır.
        /// </summary>
        protected dynamic RuntimeObjects { get; set; } = new ExpandoObject();
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        /// <summary>
        /// VirtualPLCProperty'leri.
        /// </summary>
        public List<VirtualPLCProperty> VirtualPLCProperties { get; } = new List<VirtualPLCProperty>();
        /// <summary>
        /// Arka plan thread kontrolörü.
        /// </summary>
        protected BackgroundWorker _worker;
        /// <summary>
        /// Retain olarak işaretlenmiş VirtualPLCProperty'lerin tam yolunu tutan liste.
        /// </summary>
        protected List<string> RetainPaths { get; } = new List<string>();
        /// <summary>
        /// VirtualPLCObjeleri
        /// </summary>
        public List<VirtualPLCObject> VirtualPLCObjects { get; } = new List<VirtualPLCObject>();
        /// <summary>
        /// Retain değişkenlerin kaydedileceği dosya yolu.
        /// </summary>
        public string Path { get; set; }

        public VirtualController(string path) : this(true, path)
        {
            
        }

        public VirtualController(bool init, string path)
        {
            Path = path;
            Init();
        }
        /// <summary>
        /// Controller'ın başlangıç işlemleri.
        /// </summary>
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
        /// <summary>
        /// Controller'ın başlangıç işlemleri.
        /// </summary>
        protected virtual void InitImp()
        {
         
        }
        /// <summary>
        /// Controller'ın çalışma işlemleri.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Run(object sender, DoWorkEventArgs e)
        {
            RestoreInputs();
            Process();
            SaveRetains();
        }
        /// <summary>
        /// Run işlemleri sonrası yapılacak işlemler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Input değişkenlerinin değerleri gerçek değerlere yazılır.
        /// </summary>
        private void RestoreInputs()
        {
            VirtualPLCObjects.ForEach(x => x.RestoreInputs());
        }
        /// <summary>
        /// Output değişkenleri bildirilir.
        /// </summary>
        private void SetOutputs()
        {
            VirtualPLCObjects.ForEach(x => x.SetOutputs());
        }
        /// <summary>
        /// Retain değişkenlerin tam yolunu bulur.
        /// </summary>
        private void FindRetainVariables()
        {
            ListObjectsProperties(this, string.Empty, RetainPaths);
        }
        /// <summary>
        /// Bir obje içinde bulunan tüm VirtualPLCProperty'lere erişip, retain olanların tam yolunu listeye ekler.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parentPath"></param>
        private void ListObjectsProperties(object obj, string parentPath, List<string> list)
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
                                list.Add(path);
                            }
                            else if (propValue is IVirtualPLCPropertyOwner)
                            {
                                string path = !string.IsNullOrEmpty(parentPath) && !string.IsNullOrWhiteSpace(parentPath) ? parentPath.Trim() + "." : string.Empty;
                                path += property.Name;
                                ListObjectsProperties(propValue, path, list);
                            } 
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Bir obje içinde bulunan tüm VirtualPLCProperty'lere erişip, retain olanların tam yolunu listeden çıkarır.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parentPath"></param>
        private void DelistObjectsProperties(object obj, string parentPath, List<string> list)
        {
            if (obj != null && !(obj is VirtualPLCProperty))
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
                                list.Remove(path);
                            }
                            else if (propValue is IVirtualPLCPropertyOwner)
                            {
                                string path = !string.IsNullOrEmpty(parentPath) && !string.IsNullOrWhiteSpace(parentPath) ? parentPath.Trim() + "." : string.Empty;
                                path += property.Name;
                                DelistObjectsProperties(propValue, path, list);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Input değişkenleri alındıktan sonra çalışan, esas çalışma kodlarının bulunması gereken method.
        /// </summary>
        protected virtual void Process()
        {

        }
        /// <summary>
        /// Retain olarak işaretlenmiş VirtualPLCProperty'ler kaydedilir.
        /// </summary>
        protected virtual void SaveRetains()
        {
            lock (_retainLocker)
            {
                WriteRetains();
            }
        }
        /// <summary>
        /// Retain dosya okunur, RetainSerializationData oluşturulur. RetainPaths içerisinde bulunan bilgilere göre var olan RetainSerializationData objelerinin değerleri değiştirilir ve var olmayanlar eklenir.
        /// </summary>
        private void WriteRetains()
        {
            var readDatas = File.ReadAllLines(Path);
            var retainSerializationDatas = new List<RetainSerializationData>();
            foreach (var item in readDatas)
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<RetainSerializationData>(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                    retainSerializationDatas.Add(data);
                }
                catch
                {
                    ;
                }
            }
            foreach (var path in RetainPaths)
            {
                try
                {
                    var value = ReflectionController.GetPropertyValue(this, path + ".Value");
                    var temporaryValue = ReflectionController.GetPropertyValue(this, path + ".TemporaryValue");
                    var retainSerializationData = retainSerializationDatas.FirstOrDefault(x => x.Path == path);
                    if (retainSerializationData == null)
                    {
                        var data = new RetainSerializationData() { Path = path, Value = value, TemporaryValue = temporaryValue };
                        retainSerializationDatas.Add(data);
                    }
                    else
                    {
                        retainSerializationData.Value = value;
                        retainSerializationData.TemporaryValue = temporaryValue;
                    }
                }
                catch
                {
                    ;
                }
            }
            var writeDatas = new List<string>();
            retainSerializationDatas.ForEach(data => writeDatas.Add(JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All })));
            File.WriteAllLines(Path, writeDatas);
        }
        /// <summary>
        /// RetainPaths içinde bulunan bilgilere göre dosyayı yeniden oluşturur, RetainPaths içinde olmayan objeler daha önce kaydedildiyse bu methodla birlikte onlar silinir.
        /// </summary>
        private void WriteExistingRetainsToFile()
        {
            List<string> datas = new List<string>();
            foreach (var item in RetainPaths)
            {
                try
                {
                    var value = ReflectionController.GetPropertyValue(this, item + ".Value");
                    var temporaryValue = ReflectionController.GetPropertyValue(this, item + ".TemporaryValue");
                    var data = new RetainSerializationData() { Path = item, Value = value, TemporaryValue = temporaryValue };
                    datas.Add(JsonConvert.SerializeObject(data, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
                }
                catch
                {
                    ;
                }
            }
            File.WriteAllLines(Path, datas);
        }
        /// <summary>
        /// Retain değerler okunur ve VirtualPLCProperty objelerine yazılır.
        /// </summary>
        protected virtual void RestoreRetains()
        {
            lock (_retainLocker)
            {
                try
                {
                    var datas = File.ReadAllLines(Path);
                    foreach (var item in datas)
                    {
                        try
                        {
                            var data = JsonConvert.DeserializeObject<RetainSerializationData>(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                            ReflectionController.SetPropertyValue(this, data.Path + ".Value", data.Value);
                            ReflectionController.SetPropertyValue(this, data.Path + ".TemporaryValue", data.TemporaryValue);
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
        /// <summary>
        /// Retain değerler okunur ve belirtilen VirtualPLCProperty objelerine yazılır.
        /// </summary>
        protected virtual void RestoreRetains(List<string> propertyPaths)
        {
            lock (_retainLocker)
            {
                try
                {
                    var datas = File.ReadAllLines(Path);
                    foreach (var item in datas)
                    {
                        try
                        {
                            var data = JsonConvert.DeserializeObject<RetainSerializationData>(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                            if (propertyPaths.Contains(data.Path))
                            {
                                ReflectionController.SetPropertyValue(this, data.Path + ".Value", data.Value);
                                ReflectionController.SetPropertyValue(this, data.Path + ".TemporaryValue", data.TemporaryValue);
                            }
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
        /// <summary>
        /// VirtualController'a bir runtime objesi ekler.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        protected virtual void AddRuntimeObject(string name, object obj)
        {
            if (obj == null)
                return;
            var runtimeObjects = (IDictionary<string, object>)RuntimeObjects;
            if (runtimeObjects.ContainsKey(name))
                return;
            runtimeObjects[name] = obj;
            var retainPaths = new List<string>();
            ListObjectsProperties(obj, nameof(RuntimeObjects) + "." + name, retainPaths);
            RestoreRetains(retainPaths);
            RetainPaths.AddRange(retainPaths);
        }
        /// <summary>
        /// VirtualController'daki bir runtime objesini siler.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        protected virtual void RemoveRuntimeObject(string name, object obj)
        {
            if (obj == null)
                return;
            var runtimeObjects = (IDictionary<string, object>)RuntimeObjects;
            if (!runtimeObjects.ContainsKey(name))
                return;
            runtimeObjects.Remove(name);
            DelistObjectsProperties(obj, nameof(RuntimeObjects) + "." + name, RetainPaths);
        }
        /// <summary>
        /// VirtualController'daki bir runtime objelerini temizler.
        /// </summary>
        protected virtual void ClearRuntimeObject()
        {
            var runtimeObjects = (IDictionary<string, object>)RuntimeObjects;
            foreach (var item in runtimeObjects)
            {
                DelistObjectsProperties(item.Value, nameof(RuntimeObjects) + "." + item.Key, RetainPaths);
            }
            runtimeObjects.Clear();
        }
    }
}

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
        protected List<string> RetainPaths = new List<string>();
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
            ListObjectsProperties(this, string.Empty);
        }
        /// <summary>
        /// Controller içinde bulunan tüm VirtualPLCProperty'lere erişip, retain olanların tam yolunu bulur.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parentPath"></param>
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
            List<string> datas = new List<string>();
            foreach (var item in RetainPaths)
            {
                try
                {
                    var value = ReflectionController.GetPropertyValue(this, item+".Value");
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
        /// <summary>
        /// Retain değerler okunur ve VirtualPLCProperty objelerine yazılır.
        /// </summary>
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
                        ReflectionController.SetPropertyValue(this, data.Path+".Value", data.Value);
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

using Extensions;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace Promax.Core
{
    public class Serializer
    {
        private bool _autoSerialize;
        private object _context;

        public string Path { get; set; }
        public Serializer(string path)
        {
            Path = path;
        }
        public void AutoSerialize(object context)
        {
            _context = context;
            if (context != null && context is INotifyPropertyChanged)
            {
                (context as INotifyPropertyChanged).PropertyChanged += Changed;
            }
            _autoSerialize = true;
        }
        public void DisableAutoSerialize()
        {
            _autoSerialize = false;
        }
        private void Changed(object sender, PropertyChangedEventArgs e)
        {
            if (!_autoSerialize)
                return;
            _context.Do(x => Serialize(x));
        }

        public virtual void Serialize(object context)
        {
            File.WriteAllLines(Path, new string[] { SerializeData(context) });
        }

        public virtual string SerializeData(object context)
        {
            return JsonConvert.SerializeObject(context, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }
        public virtual object DeserializeData(string context)
        {
            return JsonConvert.DeserializeObject(context, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }
        public virtual T DeserializeData<T>(string context)
        {
            return JsonConvert.DeserializeObject<T>(context, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }

        public virtual T Deserialize<T>()
        {
            var result = GetSerializedData();
            if (result.Deserialized)
                return JsonConvert.DeserializeObject<T>(result.SerializedData, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            else
                return default(T);
        }
        public virtual object Deserialize()
        {
            var result = GetSerializedData();
            if (result.Deserialized)
                return JsonConvert.DeserializeObject(result.SerializedData, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            else
                return null;
        }
        public virtual void Populate(object obj)
        {
            var result = GetSerializedData();
            if (result.Deserialized)
                JsonConvert.PopulateObject(result.SerializedData, obj, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }

        private SerializedDataResult GetSerializedData()
        {
            if (!File.Exists(Path))
                return new SerializedDataResult(false, string.Empty);
            var value = File.ReadAllLines(Path)[0];
            return new SerializedDataResult(true, value);
        }
        private class SerializedDataResult
        {
            public SerializedDataResult(bool deserialized, string serializedData)
            {
                Deserialized = deserialized;
                SerializedData = serializedData;
            }

            public bool Deserialized { get; private set; }
            public string SerializedData { get; private set; }
        }
    }
}
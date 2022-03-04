using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Promax.Core
{
    public class RetainableContainerSerializer
    {
        private bool _autoSerialize;
        private RetainableContainer _container;

        public string Path { get; set; }
        public RetainableContainerSerializer(string path)
        {
            Path = path;
        }
        public void AutoSerialize(RetainableContainer container)
        {
            _container = container;
            foreach (var item in _container.Objects)
            {
                Subscribe(item);
            }
            _container.ObjectRegistered += (con, reg) => { Subscribe(reg); };
            _container.ObjectUnregistered += (con, reg) => { Unsubscribe(reg); };
            _autoSerialize = true;
        }

        private void Subscribe(IRetainable item)
        {
            if (item != null && item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged += Changed;
            }
        }
        private void Unsubscribe(IRetainable item)
        {
            if (item != null && item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged -= Changed;
            }
        }

        public void DisableAutoSerialize()
        {
            _autoSerialize = false;
        }
        private void Changed(object sender, PropertyChangedEventArgs e)
        {
            if (!_autoSerialize)
                return;
            _container.Do(x => Save(x));
        }

        public void Save(RetainableContainer context)
        {
            List<RetainableSerializationData> serializationData = new List<RetainableSerializationData>();
            foreach (var item in context.Objects)
            {
                serializationData.Add(new RetainableSerializationData() { Id = item.RetainableId, SerializationData = JsonConvert.SerializeObject(item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }) });
            }
            File.WriteAllLines(Path, new string[] { JsonConvert.SerializeObject(serializationData, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }) });
        }
        public void Load(RetainableContainer context)
        {
            List<RetainableSerializationData> serializationDatas = JsonConvert.DeserializeObject<List<RetainableSerializationData>>(File.ReadAllLines(Path)[0], new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            foreach (var data in serializationDatas)
            {
                foreach (var item in context.Objects)
                {
                    if (data.Id == item.RetainableId)
                    {
                        JsonConvert.PopulateObject(data.SerializationData, item, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                    }
                }
            }
        }

        private class RetainableSerializationData
        {
            public string Id { get; set; }
            public string SerializationData { get; set; }
        }
    }
}

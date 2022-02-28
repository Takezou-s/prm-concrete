using Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Promax.Core
{
    public class ParameterOwnerContextSerializer
    {
        private bool _autoSerialize;
        private ParameterOwnerContext _context;

        public string Path { get; set; }
        public ParameterOwnerContextSerializer(string path)
        {
            Path = path;
        }
        public void AutoSerialize(ParameterOwnerContext context)
        {
            _context = context;
            context.ParameterOwners.ForEach(x => x.PropertyChanged += Changed);
            context.ParameterOwners.ForEach(x => x.Parameters.ForEach(y => y.DoIf(z => z is INotifyPropertyChanged, z => (z as INotifyPropertyChanged).PropertyChanged += Changed)));
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
            _context.Do(x => Save(x));
        }

        public void Save(ParameterOwnerContext context)
        {
            List<ParameterSerializationData> serializationData = new List<ParameterSerializationData>();
            foreach (var item in context.ParameterOwners)
            {
                foreach (var item1 in item.Parameters)
                {
                    serializationData.Add(new ParameterSerializationData() { Id = item.Name + "." + item1.Name, SerializationData = JsonConvert.SerializeObject(item1, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }) });
                }
            }
            File.WriteAllLines(Path, new string[] { JsonConvert.SerializeObject(serializationData, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }) });
        }
        public void Load(ParameterOwnerContext context)
        {
            List<ParameterSerializationData> serializationDatas = JsonConvert.DeserializeObject<List<ParameterSerializationData>>(File.ReadAllLines(Path)[0], new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            foreach (var data in serializationDatas)
            {
                foreach (var item in context.ParameterOwners)
                {
                    foreach (var item1 in item.Parameters)
                    {
                        if (data.Id == item.Name + "." + item1.Name)
                        {
                            JsonConvert.PopulateObject(data.SerializationData, item1, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                        }
                    }
                }
            }
        }

        private class ParameterSerializationData
        {
            public string Id { get; set; }
            public string SerializationData { get; set; }
        }
    }
}
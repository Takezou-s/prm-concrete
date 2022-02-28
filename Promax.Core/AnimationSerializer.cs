using Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Promax.Core
{
    public class AnimationSerializer : IAnimationSerializer
    {
        public AnimationSerializer(string path)
        {
            Path = path;
        }

        public AnimationSerializer() : this("saves\\Properties.txt")
        {
        }

        public string Path { get; set; }
        public void Serialize(IAnimationObject[] animationObjects)
        {
            SerializationDataContainer container = new SerializationDataContainer();
            foreach (var item in animationObjects)
            {
                var serializationData = new AnimationSerializationData();
                serializationData.AnimationObjectName = item.AnimationObjectName;
                serializationData.Serialized = JsonConvert.SerializeObject(item.Props);
                container.SerializationDatas.Add(serializationData);
            }
            File.WriteAllText(Path, JsonConvert.SerializeObject(container));
        }
        public void Deserialize(IAnimationObject[] animationObjects)
        {
            if (!File.Exists(Path))
                return;
            string serializationString = File.ReadAllText(Path);
            var container = JsonConvert.DeserializeObject<SerializationDataContainer>(serializationString);
            foreach (var item in container.SerializationDatas)
            {
                var obj = animationObjects.FirstOrDefault(x => item.AnimationObjectName.IsEqual(x.AnimationObjectName));
                obj.Do(x => JsonConvert.PopulateObject(item.Serialized, x.Props));
            }
        }
        private class SerializationDataContainer
        {
            public List<AnimationSerializationData> SerializationDatas { get; set; } = new List<AnimationSerializationData>();
        }
        private class AnimationSerializationData
        {
            public string AnimationObjectName { get; set; }
            public string Serialized { get; set; }
        }
    }
}
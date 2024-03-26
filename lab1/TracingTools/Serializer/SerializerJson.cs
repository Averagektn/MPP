using Newtonsoft.Json;
using TracerLab.Serializer;

namespace TracingTools.Serializer
{
    public class SerializerJson(Formatting formatting = Formatting.Indented) : ISerializer
    {
        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, formatting);
    }
}

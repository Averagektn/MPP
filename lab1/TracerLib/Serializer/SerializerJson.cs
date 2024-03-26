using Newtonsoft.Json;

namespace TracerLab.Serializer
{
    public class SerializerJson : ISerializer
    {
        private readonly Formatting _formatting;

        public SerializerJson(Formatting formatting = Formatting.Indented)
        {
            _formatting = formatting;
        }

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, _formatting);
    }
}

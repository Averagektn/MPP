using Newtonsoft.Json;
using System.Xml.Linq;
using TracerLab.Serializer;

namespace TracingTools.Serializer
{
    public class SerializerXML : ISerializer
    {
        public string Serialize(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var node = JsonConvert.DeserializeXNode(json, "root") ?? new XDocument();

            return node.ToString();
        }
    }
}

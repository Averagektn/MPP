using Newtonsoft.Json;
using System.Xml.Linq;

namespace TracerLab.Serializer
{
    public class SerializerXML : ISerializer
    {
        public string Serialize(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var node = JsonConvert.DeserializeXNode(json, "") ?? new XDocument();

            return node.ToString();
        }
    }
}

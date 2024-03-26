using System.IO;

namespace TracerLab.Serializer
{
    public class SerializedWriter : IWriter
    {
        private readonly ISerializer _serializer;

        public SerializedWriter(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void Write(object obj, TextWriter writer)
        {
            writer.Write(_serializer.Serialize(obj));
        }
    }
}

using System.IO;

namespace TracerLab.Serializer
{
    public interface IWriter
    {
        void Write(object obj, TextWriter writer);
    }
}

namespace TracingTools.Serializer
{
    public interface IWriter
    {
        void Write(object obj, TextWriter writer);
    }
}

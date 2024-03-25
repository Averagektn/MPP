namespace TracerLab.Tracing
{
    public class ThreadData
    {
        public readonly List<(MethodData MethodData, ThreadData? ThreadData)> Data = [];
    }
}

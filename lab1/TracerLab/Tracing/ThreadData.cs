namespace TracerLab.Tracing
{
    public record class ThreadData(int ThreadId, string Time, List<MethodData> Methods);
}

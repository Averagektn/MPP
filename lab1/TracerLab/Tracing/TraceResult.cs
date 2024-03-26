namespace TracerLab.Tracing
{
    public class TraceResult(ICollection<ThreadData> threadsData)
    {
        public IReadOnlyList<ThreadData> Threads => [.. threadsData];
    }
}

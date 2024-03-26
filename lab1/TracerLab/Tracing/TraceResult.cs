using System.Collections.Immutable;

namespace TracerLab.Tracing
{
    public class TraceResult(IDictionary<int, List<MethodData>> pairs)
    {
        public ImmutableDictionary<int, List<MethodData>> Result => pairs.ToImmutableDictionary();
    }
}

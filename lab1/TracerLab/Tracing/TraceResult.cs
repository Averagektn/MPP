using System.Collections.Immutable;
using DictionaryData = (System.Collections.Generic.List<TracerLab.Tracing.ThreadData> ThreadData, int Time);

namespace TracerLab.Tracing
{
    public class TraceResult(IDictionary<int, DictionaryData> Pairs)
    {
        public ImmutableDictionary<int, DictionaryData> Result => Pairs.ToImmutableDictionary();

        public override string ToString()
        {
            return base.ToString()!;
        }
    }
}

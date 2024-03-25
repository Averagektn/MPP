using System.Collections.Concurrent;
using DictionaryData = (System.Collections.Generic.List<TracerLab.Tracing.ThreadData> ThreadData, int Time);

namespace TracerLab.Tracing
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, DictionaryData> _traceData = [];

        public TraceResult GetTraceResult() => new TraceResult(_traceData);

        public void StartTrace()
        {
            throw new NotImplementedException();
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }
    }
}

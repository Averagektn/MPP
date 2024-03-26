using System.Collections.Concurrent;
using System.Diagnostics;

namespace TracingTools.Tracing
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, List<MethodData>> _traceData = [];
        private readonly ConcurrentDictionary<int, Stack<MethodData>> _threadStack = [];

        public TraceResult GetTraceResult()
            => new(_traceData.Select(kv => new ThreadData(kv.Key, $"{kv.Value.Sum(v => v.Time)}ms", kv.Value)).ToList());

        public void StartTrace()
        {
            var callingMethod = new StackFrame(1).GetMethod()!;
            var callingMethodName = callingMethod.Name;
            var callingClassName = callingMethod.DeclaringType!.Name;
            var method = new MethodData(callingMethodName, callingClassName);
            var threadId = Environment.CurrentManagedThreadId;

            // If first element just push to stack and dictionary
            if (!_threadStack.TryGetValue(threadId, out Stack<MethodData>? value))
            {
                _traceData[threadId] = [method];
                _threadStack[threadId] = new();
                _threadStack[threadId].Push(method);
            }
            // If value already exists, find position by using stack
            else
            {
                if (value.Count == 0)
                {
                    _traceData[threadId].Add(method);
                }
                else
                {
                    var methodData = value.Peek();
                    methodData.Methods.Add(method);
                }

                value.Push(method);
            }
            method.StartTimer();
        }

        public void StopTrace()
        {
            var threadId = Environment.CurrentManagedThreadId;
            var method = _threadStack[threadId].Pop();
            method.StopTimer();
        }
    }
}

using System.Collections.Concurrent;
using System.Diagnostics;

namespace TracerLab.Tracing
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, List<MethodData>> _traceData = [];
        private readonly ConcurrentDictionary<int, Stack<MethodData>> _threadStack = [];

        public TraceResult GetTraceResult() => new(_traceData);

        public void StartTrace()
        {
            var callingMethod = new StackFrame(1).GetMethod()!;
            var callingMethodName = callingMethod.Name;
            var callingClassName = callingMethod.DeclaringType!.Name;
            var method = new MethodData(callingMethodName, callingClassName);
            var threadId = Environment.CurrentManagedThreadId;

            // If first element just push to stack and dictionary
            if (!_threadStack.ContainsKey(threadId))
            {
                _traceData[threadId] = [method];
                _threadStack[threadId] = new();
                _threadStack[threadId].Push(method);
            }
            // If value already exists, find position by using stack
            else
            {
                if (_threadStack[threadId].Count == 0)
                {
                    _traceData[threadId].Add(method);
                }
                else
                {
                    var methodData = _threadStack[threadId].Peek();
                    methodData.Methods.Add(method);
                }

                _threadStack[threadId].Push(method);
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

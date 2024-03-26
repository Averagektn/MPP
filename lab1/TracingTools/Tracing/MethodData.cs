using System.Diagnostics;

namespace TracingTools.Tracing
{
    public class MethodData(string methodName, string className)
    {
        public readonly string MethodName = methodName;
        public readonly string ClassName = className;
        public long Time { get; private set; }
        public List<MethodData> Methods { get; } = [];
        private readonly Stopwatch _stopwatch = new();

        public void StartTimer()
        {
            _stopwatch.Start();
        }

        public void StopTimer()
        {
            _stopwatch.Stop();
            Time = _stopwatch.ElapsedMilliseconds;
        }
    }
}

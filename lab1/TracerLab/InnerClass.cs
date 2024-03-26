using TracingTools.Tracing;

namespace TracerLab
{
    public class InnerClass
    {
        public readonly ITracer _tracer;

        internal InnerClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(new Random().Next(1, 100));

            AnotherMethod();
            AnotherMethod();

            _tracer.StopTrace();
        }

        private void AnotherMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(new Random().Next(1, 100));

            _tracer.StopTrace();
        }
    }
}

using TracingTools.Tracing;

namespace TracerLab
{
    internal class OuterClass
    {
        private readonly InnerClass _innerClass;
        private readonly ITracer _tracer;

        internal OuterClass(ITracer tracer)
        {
            _tracer = tracer;
            _innerClass = new InnerClass(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            ///
            _innerClass.InnerMethod();
            ///
            _tracer.StopTrace();
        }
    }
}

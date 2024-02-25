namespace TracerLab
{
    internal class InnerClass
    {
        private readonly ITracer _tracer;

        internal InnerClass(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            _tracer.StopTrace();
        }
    }
}

using FluentAssertions;
using TracingTools.Tracing;

namespace Tests
{
    public class TracerTests
    {
        private readonly Tracer _tracer;

        public TracerTests()
        {
            _tracer = new Tracer();
        }

        [Fact]
        public void Test_EmptyTraceResult()
        {
            var res = _tracer.GetTraceResult();

            Assert.Empty(res.Threads);
        }

        [Fact]
        public void Test_SingleTopMethod()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();

            var res = _tracer.GetTraceResult();

            Assert.Single(res.Threads[0].Methods);
        }

        [Fact]
        public void Test_NestedMethods()
        {
            _tracer.StartTrace();
            _tracer.StartTrace();
            _tracer.StopTrace();
            _tracer.StopTrace();

            var res = _tracer.GetTraceResult();

            Assert.Single(res.Threads);
            Assert.Single(res.Threads[0].Methods);
        }

        [Fact]
        public void Test_MultipleTopMethods()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
            _tracer.StartTrace();
            _tracer.StopTrace();

            var res = _tracer.GetTraceResult();

            res.Threads[0].Methods.Should().HaveCount(2);
        }

        [Fact]
        public void Test_MultipleThreads()
        {
            var firstThread = new Thread(() =>
            {
                _tracer.StartTrace();
                _tracer.StopTrace();
            });

            var secondThread = new Thread(() =>
            {
                _tracer.StartTrace();
                _tracer.StopTrace();
            });

            firstThread.Start();
            secondThread.Start();

            _tracer.StartTrace();
            _tracer.StopTrace();

            firstThread.Join();
            secondThread.Join();

            var res = _tracer.GetTraceResult();

            res.Threads.Should().HaveCount(3);
        }

        [Fact]
        public void Test_NewThreadInsideMethodCall()
        {
            var firstThread = new Thread(() =>
            {
                _tracer.StartTrace();
                var thread = new Thread(() =>
                {
                    _tracer.StartTrace();
                    _tracer.StopTrace();
                });
                thread.Start();
                thread.Join();
                _tracer.StopTrace();
            });
            firstThread.Start();
            firstThread.Join();

            var res = _tracer.GetTraceResult();

            res.Threads.Should().HaveCount(2);
        }
    }
}
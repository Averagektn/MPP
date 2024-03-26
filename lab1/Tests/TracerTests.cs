using FluentAssertions;
using TracingTools.Tracing;

namespace Tests
{
    public class TracerTests
    {
        [Fact]
        public void Test_EmptyTraceResult()
        {
            var tracer = new Tracer();
            var res = tracer.GetTraceResult();
            Assert.Empty(res.Threads);
        }

        [Fact]
        public void Test_SingleTopMethod()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            tracer.StopTrace();
            var res = tracer.GetTraceResult();
            Assert.Single(res.Threads[0].Methods);
        }

        [Fact]
        public void Test_NestedMethods()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            tracer.StartTrace();
            tracer.StopTrace();
            tracer.StopTrace();

            var res = tracer.GetTraceResult();
            Assert.Single(res.Threads);
            Assert.Single(res.Threads[0].Methods);
        }

        [Fact]
        public void Test_MultipleTopMethods()
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            tracer.StopTrace();
            tracer.StartTrace();
            tracer.StopTrace();

            var res = tracer.GetTraceResult();
            res.Threads[0].Methods.Should().HaveCount(2);
        }

        [Fact]
        public void Test_MultipleThreads()
        {
            var tracer = new Tracer();
            var firstThread = new Thread(() =>
            {
                tracer.StartTrace();
                tracer.StopTrace();
            });

            var secondThread = new Thread(() =>
            {
                tracer.StartTrace();
                tracer.StopTrace();
            });

            firstThread.Start();
            secondThread.Start();

            tracer.StartTrace();
            tracer.StopTrace();

            firstThread.Join();
            secondThread.Join();

            var res = tracer.GetTraceResult();
            res.Threads.Should().HaveCount(3);
        }

        [Fact]
        public void Test_NewThreadInsideMethodCall()
        {
            var tracer = new Tracer();

            var firstThread = new Thread(() =>
            {
                tracer.StartTrace();
                var thread = new Thread(() =>
                {
                    tracer.StartTrace();
                    tracer.StopTrace();
                });
                thread.Start();
                thread.Join();
                tracer.StopTrace();
            });
            firstThread.Start();
            firstThread.Join();

            var res = tracer.GetTraceResult();
            res.Threads.Should().HaveCount(2);
        }
    }
}
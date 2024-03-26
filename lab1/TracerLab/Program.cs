using TracerLab;
using TracingTools.Serializer;
using TracingTools.Tracing;

var tracer = new Tracer();
var outerClass = new OuterClass(tracer);

outerClass.MyMethod();
var res = tracer.GetTraceResult();

var serializer = new SerializerXML();
var writer = new SerializedWriter(serializer);

writer.Write(res, Console.Out);

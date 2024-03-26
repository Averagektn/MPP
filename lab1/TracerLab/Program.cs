using Newtonsoft.Json;
using TracerLab;
using TracerLab.Tracing;

var tracer = new Tracer();
var outerClass = new OuterClass(tracer);
var innerClass = new InnerClass(tracer);
outerClass.MyMethod();
innerClass.InnerMethod();
var res = tracer.GetTraceResult();
Console.WriteLine(JsonConvert.SerializeObject(res, Formatting.Indented));

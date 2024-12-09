using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Prid;

var summary = BenchmarkRunner.Run<PridBenchmarks>();

[MemoryDiagnoser]
// [RPlotExporter]
public class PridBenchmarks
{
    [Params(
        "This is fun!",
        "I can convert sentences to IDs.",
        "Not too long sentences.",
        "What use is this?",
        "It is just entertaining."
    )]
    public string EncodeMe;

    [Params(
        "741515f6117000000000000000000000",
        "1ca17c017aae1275e177e17ce5701d50",
        "17077002-0176-5e17-7e17-ce5000000000",
        "aaa4a761-5e15-7415-0000-000000000000",
        "1c1a17c017aae1275e177e17ce5701d5"
    )]
    public string DecodeMe;

    Guid _guid;

    [GlobalSetup]
    public void Setup() => _guid = Guid.Parse(DecodeMe);

    [Benchmark]
    public void Encode_strings() => Encoder.Encode(EncodeMe);

    [Benchmark]
    public void Decode_guids() => Decoder.Decode(_guid);
}

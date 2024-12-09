using System;
using System.Collections.Generic;
using System.Linq;
using Prid;

switch (args)
{
    case { Length: 0 }:
        Console.WriteLine("Please enter at least one string");
        WriteHelp();
        return;
    case var help when help[0] == "-h" || help[0] == "--help":
        WriteHelp();
        return;
    case var _ when args[0] == "-d":
        Console.WriteLine("Decodes to");
        foreach (var (input, output) in DecodePrids(args.Skip(1)))
        {
            Console.WriteLine($"{input} - {output}");
        }
        return;
    default:
        Console.WriteLine("Encodes to");
        foreach (var (input, output) in EncodePrids(args))
        {
            Console.WriteLine($"{input} - {output}");
        }
        return;
}

static void WriteHelp()
{
    Console.WriteLine("Usage: prid [-d] <string>...");
    Console.WriteLine("  -d  Decode the given strings");
}

static IEnumerable<(string input, string output)> DecodePrids(
    IEnumerable<string> args
) => args
    .Select(input =>
        (
            input,
            Guid.TryParse(input, out var guid)
                ? Decoder.Decode(guid)
                : "unable"
        )
    );

static IEnumerable<(string input, string output)> EncodePrids(
    IEnumerable<string> args
) => args
    .Select(
        value => (
            input: value,
            output: Encoder
                .Encode(value)
                .Match(
                    guid => guid.ToString(),
                    error => error.Value.Message
                )
        )
    );

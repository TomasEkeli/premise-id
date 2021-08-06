using System;
using System.Collections.Generic;
using System.Linq;
using premise_id;

namespace app
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter at least one string");
                return;
            }

            IEnumerable<(string input, string output)> result;
            if (args[0] == "-d")
            {
                Console.WriteLine("Decodes to");
                result = DecodePrids(args.Skip(1));
            }
            else
            {
                Console.WriteLine("Encodes to");
                result = EncodePrids(args);
            }

            foreach (var p in result)
            {
                Console.WriteLine($"{p.input} - {p.output}");
            }
        }

        static IEnumerable<(string input, string output)> DecodePrids(
            IEnumerable<string> args
        )
        {
            return args
                .Select(input =>
                    (
                        input,
                        Guid.TryParse(input, out var guid)
                            ? prid.Deconvert(guid)
                            : "unable"
                    )
                );
        }

        static IEnumerable<(string input, string output)> EncodePrids(
            IEnumerable<string> args
        )
        {
            return args
                .Select(
                    _ => (
                        input: _,
                        output: prid.Can_convert_all_characters_in(_)
                            ? prid.Convert(_).ToString()
                            : "unable"
                    )
                );
        }
    }
}

using System;
using System.Linq;
using OneOf;

namespace Prid;

public static class Encoder
{
    public static Span<char> Find_problematic_characters(
        string candidate
    ) =>
        candidate
            .ToLowerInvariant()
            .ToCharArray()
            .Where(_ => !Map.Mappings.ContainsKey(_))
            .ToArray();

    public static OneOf<Guid, string> Encode(string value)
    {
        var problematic = Find_problematic_characters(value);
        if (problematic.Length > 0)
        {
            return $@"Contains unsupported characters: {problematic}.";
        }
        var as_legal_characters = Convert_to_legal(value);

        if (as_legal_characters.Length > 31)
        {
            return $@"The value '{value
                }' cannot fit in a GUID, it is too large.";
        }

        return new Guid(
            as_legal_characters.PadRight(32, '0')
        );
    }

    static string Convert_to_legal(string value) =>
        string
            .Concat(
                value
                    .ToLowerInvariant()
                    .ToArray()
                    .Select(_ => Map.Mappings[_])
            );
}

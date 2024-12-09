using System;
using System.Linq;

namespace Prid;

public static class Encoder
{
    public static bool Can_convert_all_characters_in(string candidate) =>
        candidate
            .ToLowerInvariant()
            .ToCharArray()
            .All(_ => Map.Mappings.ContainsKey(_));

    public static Guid Encode(string value)
    {
        if (!Can_convert_all_characters_in(value))
        {
            throw new ValueContainsUnsupportedCharacters(value);
        }
        var as_legal_characters = Convert_to_legal(value);

        if (as_legal_characters.Length > 31)
        {
            throw new ValueTooLongToFitInGuid(value);
        }

        return new Guid(as_legal_characters.PadRight(32, '0'));
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

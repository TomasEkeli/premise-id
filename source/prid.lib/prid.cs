using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prid;

public static class Prid
{
    static readonly Dictionary<char, string> _mappings =new()
    {
        {'0', "0"},
        {'1', "1"},
        {'2', "2"},
        {'3', "3"},
        {'4', "4"},
        {'5', "5"},
        {'6', "6"},
        {'7', "7"},
        {'8', "8"},
        {'9', "9"},
        {'a', "a"},
        {'b', "b"},
        {'c', "c"},
        {'d', "d"},
        {'e', "e"},
        {'f', "f"},
        {'g', "6"},
        {'h', "4"},
        {'i', "1"},
        {'j', "3"},
        {'k', "1c"},
        {'l', "2"},
        {'m', "a4"},
        {'n', "17"},
        {'o', "0"},
        {'p', "9"},
        {'r', "12"},
        {'s', "5"},
        {'t', "7"},
        {'u', "61"},
        {'v', "aa"},
        {'w', "aaa"},
        {'x', "8"},
        {'y', "14"},
        {' ', ""},
        {'-', ""},
        {'_', ""},
        {'(', ""},
        {')', ""},
        {'[', ""},
        {']', ""},
        {'{', ""},
        {'}', ""},
        {',', ""},
        {'.', ""},
        {':', ""},
        {'!', ""},
        {'?', ""},
    };

    public static bool Can_convert_all_characters_in(string candidate) =>
        candidate
            .ToLowerInvariant()
            .ToCharArray()
            .All(_ => _mappings.ContainsKey(_));

    public static Guid Convert(string value)
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

    public static string Decode(Guid prid)
    {
        var as_string = prid
            .ToString("N")
            .ToLowerInvariant();

        var reverse_mapping = _mappings
            .Skip(10)
            .ToList()
            .OrderByDescending(_ => _.Value.Length);

        var sb = new StringBuilder();

        for (int i = 0; i < as_string.Length; i++)
        {
            if (as_string[i] == 'a'
                && i + 2 < as_string.Length
                && as_string[i + 1] == 'a'
                && as_string[i + 2] == 'a')
            {
                sb.Append('w');
                i += 2;
                continue;
            }

            if (i + 1 < as_string.Length
                && reverse_mapping
                    .Any(_ =>
                        _.Value == as_string.Substring(i, 2)
                    )
            )
            {
                sb.Append(
                    reverse_mapping
                        .First(_ =>
                            _.Value == as_string.Substring(i, 2)
                        )
                        .Key
                );

                i++;
                continue;
            }

            sb.Append(
                reverse_mapping
                    .First(_ => _.Value == $"{as_string[i]}")
                    .Key
            );
        }

        return sb.ToString().TrimEnd('o');
    }

    static string Convert_to_legal(string value) =>
        string
            .Concat(
                value
                    .ToLowerInvariant()
                    .ToArray()
                    .Select(_ => _mappings[_])
            );
}

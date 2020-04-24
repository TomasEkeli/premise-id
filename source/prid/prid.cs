using System.Collections.Generic;
using System.Linq;

namespace premise_id
{
    public static class prid
    {
        public static string legal_guid_characters = "0123456789abcdef";

        public static IEnumerable<(char, string)> mappings = new []
        {
            ('g', "6"),
            ('h', "4"),
            ('i', "1"),
            ('k', "1c"),
            ('l', "2"),
            ('m', "a4"),
            ('n', "17"),
            ('o', "0"),
            ('p', "9"),
            ('r', "12"),
            ('s', "5"),
            ('t', "7"),
            ('u', "61"),
            ('v', "aa"),
            ('w', "aaa"),
            ('x', "8"),
            ('y', "14"),
            (' ', ""),
            ('-', ""),
            (',', ""),
            ('.', ""),
            (':', ""),
        };

        public static bool Can_convert_all_characters_in(string candidate)
        {
            var allowed_characters = mappings.Select(_ => _.Item1);

            return candidate
                .ToLowerInvariant()
                .ToCharArray()
                .All(_ =>
                    allowed_characters.Contains(_)
                    || legal_guid_characters.Contains(_)
                );
        }
    }
}
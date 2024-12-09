using System;
using System.Linq;
using System.Text;

namespace Prid;

public static class Decoder
{
    public static string Decode(Guid prid)
    {
        var as_string = prid
            .ToString("N")
            .ToLowerInvariant();

        var reverse_mapping = Map.Mappings
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
}

using System;

namespace Prid;

public static class Decoder
{
    public static string Decode(Guid prid)
    {
        ReadOnlySpan<char> as_span = prid
            .ToString("N")
            .ToLowerInvariant()
            .AsSpan();

        var result = new char[as_span.Length];
        var result_index = 0;
        for (int i = 0; i < as_span.Length; i++)
        {
            var range_to = i + 3;
            if (i + 2 < as_span.Length
                && as_span[i .. range_to].SequenceEqual("aaa"))
            {
                result[result_index++] = 'w';
                i += 2;
                continue;
            }

            range_to = i + 2;
            if (range_to < as_span.Length)
            {
                var candidate = as_span[i .. range_to];

                var double_hit = Map
                    .ReverseMappings
                    .TryGetValue(
                        candidate.ToString(),
                        out var double_match
                    );

                if (double_hit)
                {
                    result[result_index++] = double_match;
                    i++;
                    continue;
                }
            }

            var hit = Map
                .ReverseMappings
                .TryGetValue(
                    as_span[i].ToString(),
                    out var single_match
                );

            if (hit)
            {
                result[result_index++] = single_match;
            }
        }

        return new string(result, 0, result_index).TrimEnd('o');
    }
}
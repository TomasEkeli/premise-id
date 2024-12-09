using Xunit;
using FluentAssertions;
using System;

namespace Prid.Tests;

public class DecoderTests
{

    [Theory]
    [InlineData("thisisfun", "741515f6117000000000000000000000")]
    [InlineData("kanconvertsentencestoids", "1ca17c017aae1275e177e17ce5701d50")]
    [InlineData("nottoolongsentences", "17077002-0176-5e17-7e17-ce5000000000")]
    [InlineData("whatuseisthis", "aaa4a761-5e15-7415-0000-000000000000")]
    [InlineData("nisjustentertaining", "17153615-7e17-7e12-7a11-711760000000")]
    public void When_decoding_a_guid_to_a_string(
        string expected,
        string input)
    {
        Guid the_prid = new(input);

        var result = Decoder.Decode(the_prid);

        result
            .Should()
            .Be(expected);
    }
}

using Xunit;
using FluentAssertions;
using System;

namespace Prid.Tests;

public class EncoderTests
{
    [Fact]
    public void When_checking_a_string_with_unsupported_characters()
    {
        const string unsupported = "æøå";

        Encoder.Can_convert_all_characters_in(unsupported)
            .Should()
            .Be(false);
    }

    [Fact]
    public void When_checking_a_string_with_supported_characters()
    {
        const string supported = "stuv";

        Encoder.Can_convert_all_characters_in(supported)
            .Should()
            .Be(true);
    }

    [Fact]
    public void When_checking_a_string_with_uppercase_supported_characters()
    {
        const string supported = "STUV";

        Encoder.Can_convert_all_characters_in(supported)
            .Should()
            .Be(true);
    }

    [Fact]
    public void When_checking_a_string_with_characters_allowed_in_guid()
    {
        const string supported = "0987654321abcdef";

        Encoder.Can_convert_all_characters_in(supported)
            .Should()
            .Be(true);
    }

    [Fact]
    public void When_converting_a_too_long_string()
    {
        const string too_long = "abcabcabacbacbdaccbaadbadcadbadcbacaadadcbcadbcadcbacd";

        var exception = Record.Exception(
            () => _ = Encoder.Encode(too_long)
        );

        exception
            .Should()
            .BeOfType<ValueTooLongToFitInGuid>();

        exception
            .Message
            .Should()
            .Be(
                $"The value '{too_long}' cannot fit in a GUID, it is too large."
            );
    }

    [Fact]
    public void When_converting_a_string_with_unsupported_characters()
    {
        const string unsupported = "kamelåså";

        var exception = Record.Exception(
            () => _ = Encoder.Encode(unsupported)
        );

        exception
            .Should()
            .BeOfType<ValueContainsUnsupportedCharacters>();

        exception
            .Message
            .Should()
            .Be(
                $"The value '{unsupported}' contains unsupported characters."
            );
    }

    [Theory]
    [InlineData("This is fun!", "741515f6117000000000000000000000")]
    [InlineData("I can convert sentences to IDs.", "1ca17c017aae1275e177e17ce5701d50")]
    [InlineData("Not too long sentences.", "17077002-0176-5e17-7e17-ce5000000000")]
    [InlineData("What use is this?", "aaa4a761-5e15-7415-0000-000000000000")]
    [InlineData("It is just entertaining.", "17153615-7e17-7e12-7a11-711760000000")]
    public void When_converting_a_string_with_supported_characters(
        string input,
        string expected
    )
    {
        Guid the_expected_guid = new Guid(expected);

        Guid result = Encoder.Encode(input);

        result
            .Should()
            .Be(the_expected_guid);
    }
}

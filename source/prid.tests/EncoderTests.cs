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

        Encoder
            .Find_problematic_characters(unsupported)
            .ToString()
            .Should()
            .Contain(unsupported);
    }

    [Fact]
    public void When_checking_a_string_with_supported_characters()
    {
        const string supported = "stuv";

        Encoder
            .Find_problematic_characters(supported)
            .ToString()
            .Should()
            .BeEmpty();
    }

    [Fact]
    public void When_checking_a_string_with_uppercase_supported_characters()
    {
        const string supported = "STUV";

        Encoder
            .Find_problematic_characters(supported)
            .ToString()
            .Should()
            .BeEmpty();
    }

    [Fact]
    public void When_checking_a_string_with_characters_allowed_in_guid()
    {
        const string supported = "0987654321abcdef";

        Encoder
            .Find_problematic_characters(supported)
            .ToString()
            .Should()
            .BeEmpty();
    }

    [Fact]
    public void When_converting_a_too_long_string() =>
        Encoder
            .Encode("abcabcabacbacbdaccbaadbadcadbadcbacaadadcbcadbcadcbacd")
            .Switch(
                surprise_success => throw new InvalidOperationException(),
                expected_error =>
                {
                    expected_error
                        .Should()
                        .Be(
                            "The value 'abcabcabacbacbdaccbaadbadcadbadcbacaadadcbcadbcadcbacd' "
                            + "cannot fit in a GUID, it is too large."
                        );
                }
            );

    [Fact]
    public void When_converting_a_string_with_unsupported_characters() =>
        Encoder
            .Encode("kamelåså")
            .Switch(
                surprise_success => throw new InvalidOperationException(),
                expected_error =>
                {
                    expected_error
                        .Should()
                        .Be(
                            $"Contains unsupported characters: åå."
                        );
                }
            );

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

        Guid result = Encoder.Encode(input).AsT0;

        result
            .Should()
            .Be(the_expected_guid);
    }
}

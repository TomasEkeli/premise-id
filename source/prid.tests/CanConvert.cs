using Xunit;
using FluentAssertions;

namespace premise_id.tests
{
    public class CanConvert
    {
        [Fact]
        public void When_checking_a_string_with_unsupported_characters()
        {
            var unsupported = "æøå";

            prid.Can_convert_all_characters_in(unsupported)
                .Should()
                .Be(false);
        }

        [Fact]
        public void When_checking_a_string_with_supported_characters()
        {
            var unsupported = "stuv";

            prid.Can_convert_all_characters_in(unsupported)
                .Should()
                .Be(true);
        }

        [Fact]
        public void When_checking_a_string_with_uppercase_supported_characters()
        {
            var unsupported = "STUV";

            prid.Can_convert_all_characters_in(unsupported)
                .Should()
                .Be(true);
        }

        [Fact]
        public void When_checking_a_string_with_characters_allowed_in_guid()
        {
            var unsupported = "0987654321abcdef";

            prid.Can_convert_all_characters_in(unsupported)
                .Should()
                .Be(true);
        }
    }
}

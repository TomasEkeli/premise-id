using System;

namespace Prid;

[Serializable]
public class UnsupportedCharacters(string value)
: Exception(
    $@"The value '{value
        }' contains unsupported characters."
)
{
}

using System;

namespace Prid;

[Serializable]
public class UnsupportedCharacters(string value)
: Exception(
    $@"Contains unsupported characters: {value}."
)
{
}

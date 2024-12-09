using System;

namespace Prid;

[Serializable]
public class ValueContainsUnsupportedCharacters : Exception
{
    public ValueContainsUnsupportedCharacters(string value)
    : base($"The value '{value}' contains unsupported characters.")
    {
    }
}

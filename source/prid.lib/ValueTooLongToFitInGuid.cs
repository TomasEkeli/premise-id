using System;

namespace Prid;

[Serializable]
public class ValueTooLongToFitInGuid : Exception
{
    public ValueTooLongToFitInGuid(string value)
    : base($"The value '{value}' cannot fit in a GUID, it is too large.")
    {
    }
}
using System;

namespace Prid;

[Serializable]
public class TooLong(string value)
: Exception(
    $@"The value '{value
    }' cannot fit in a GUID, it is too large."
)
{
}
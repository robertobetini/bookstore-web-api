using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class InvalidImageTypeException : Exception
{
    public string? ContentType { get; init; }

    public InvalidImageTypeException(string? contentType) : base($"Invalid image type: {contentType}.")
    {
        ContentType = contentType;
    }

    protected InvalidImageTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
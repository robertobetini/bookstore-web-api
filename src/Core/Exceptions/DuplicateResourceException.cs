namespace Core.Exceptions;

[Serializable]
public class DuplicateResourceException : Exception
{
    private const string DEFAULT_ERROR_MESSAGE = "{0} already exists.";

    public readonly string Resource;

    public DuplicateResourceException(string resource = "Resource") 
        : base(string.Format(DEFAULT_ERROR_MESSAGE, resource))
    {
        Resource = resource;
    }
}
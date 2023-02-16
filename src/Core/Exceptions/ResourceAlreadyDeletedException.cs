namespace Core.Exceptions;

[Serializable]
public abstract class ResourceAlreadyDeletedException : Exception
{
    protected const string DEFAULT_ERROR_MESSAGE = "{0} is gone.";

    protected readonly string Resource;

    public ResourceAlreadyDeletedException(string resource = "Resource") : base(string.Format(DEFAULT_ERROR_MESSAGE, resource))
    {
        Resource = resource;
    }
}

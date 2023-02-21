namespace Core.Exceptions;

[Serializable]
public class OwnerAlreadyExists : DuplicateResourceException
{
    private const string RESOURCE = "Owner";

    public OwnerAlreadyExists() : base(RESOURCE) { }
}
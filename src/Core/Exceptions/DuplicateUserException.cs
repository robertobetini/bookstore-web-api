namespace Core.Exceptions;

[Serializable]
public class DuplicateUserException : DuplicateResourceException
{
    private const string RESOURCE = "User";

    public DuplicateUserException() : base(RESOURCE) { }
}
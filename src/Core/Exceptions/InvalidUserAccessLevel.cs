namespace Core.Exceptions;

[Serializable]
public class InvalidUserAccessLevel : BusinessException
{
    private const string DEFAULT_ERROR_MESSAGE = "Invalid user access level provided.";

    public InvalidUserAccessLevel() : base(DEFAULT_ERROR_MESSAGE) { }
}
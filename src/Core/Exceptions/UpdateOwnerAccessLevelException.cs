namespace Core.Exceptions;

[Serializable]
public class UpdateOwnerAccessLevelException : BusinessException
{
    private const string DEFAULT_ERROR_MESSAGE = "Can't update owner access level.";

    public UpdateOwnerAccessLevelException() : base(DEFAULT_ERROR_MESSAGE)
    {
    }
}
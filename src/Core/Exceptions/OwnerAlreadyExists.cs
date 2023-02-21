namespace Core.Exceptions;

[Serializable]
public class OwnerAlreadyExists : BusinessException
{
    private const string DEFAULT_ERROR_MESSAGE = "Can't have more than one owner, ownership is already claimed.";

    public OwnerAlreadyExists() : base(DEFAULT_ERROR_MESSAGE) { }
}
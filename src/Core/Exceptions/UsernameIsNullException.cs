namespace Core.Exceptions;

[Serializable]
public class UsernameIsNullException : BusinessException
{
    private const string DEFAULT_ERROR_MESSAGE = "Provided username is null or empty.";

    public UsernameIsNullException() : base(DEFAULT_ERROR_MESSAGE) { }
}
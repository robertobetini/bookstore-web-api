namespace Core.Exceptions;

[Serializable]
public class InvalidQuantityToUpdateException : BusinessException
{
    private const string DEFAULT_ERROR_MESSAGE = "Book quantity must be greater than 0.";

    public InvalidQuantityToUpdateException() : base(DEFAULT_ERROR_MESSAGE) { }
}
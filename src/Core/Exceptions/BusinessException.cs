namespace Core.Exceptions;

[Serializable]
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}

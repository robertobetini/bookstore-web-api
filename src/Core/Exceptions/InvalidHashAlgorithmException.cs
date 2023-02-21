namespace Core.Exceptions;

[Serializable]
public class InvalidHashAlgorithmException : Exception
{
    private const string DEFAULT_ERROR_MESSAGE = "Invalid hash algorithm: {0}.";

    public string AlgorithmName { get; init; }

    public InvalidHashAlgorithmException(string algorithmName)
        : base(string.Format(DEFAULT_ERROR_MESSAGE, algorithmName))
    {
        AlgorithmName = algorithmName;
    }
}
namespace Core.Exceptions;

public class NullEnvironmentVariableException : Exception
{
    private const string DEFAULT_ERROR_MESSAGE = "Environment variable {0} value is null.";

    public string VariableKey;

    public NullEnvironmentVariableException(string variableKey)
        : base(string.Format(DEFAULT_ERROR_MESSAGE, variableKey))
    {
        VariableKey = variableKey;
    }
}

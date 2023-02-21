using Core.Exceptions;

namespace Core.Providers;

public static class EnvironmentVariableProvider
{
    public static string GetValue(string variableKey)
    {
        var variableValue = Environment.GetEnvironmentVariable(variableKey);
        if (string.IsNullOrEmpty(variableValue))
        {
            throw new NullEnvironmentVariableException(variableKey);
        }

        return variableValue;
    }

    public static bool TryGetValue(string variableKey, out string? variableValue)
    {
        try
        {
            variableValue = GetValue(variableKey);
            return true;
        }
        catch (NullEnvironmentVariableException)
        {
            variableValue = default;
            return false;
        }
    }
}

using Core.Exceptions;
using Core.Providers.Interfaces;

namespace Core.Providers;

public class EnvironmentVariableProvider : IEnvironmentVariableProvider
{
    public string GetValue(string variableKey)
    {
        var variableValue = Environment.GetEnvironmentVariable(variableKey);
        if (string.IsNullOrEmpty(variableValue))
        {
            throw new NullEnvironmentVariableException(variableKey);
        }

        return variableValue;
    }

    public bool TryGetValue(string variableKey, out string? variableValue)
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

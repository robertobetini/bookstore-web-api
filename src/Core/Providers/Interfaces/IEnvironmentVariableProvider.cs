namespace Core.Providers.Interfaces;

public interface IEnvironmentVariableProvider
{
    string GetValue(string variableKey);
    bool TryGetValue(string variableKey, out string? variableValue);
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

public class CustomConfiguration : IConfiguration
{
    private readonly Dictionary<string, string> _settings;

    public CustomConfiguration(Dictionary<string, string> settings)
    {
        _settings = settings;
    }

    public IConfigurationSection GetSection(string key)
    {
        if (_settings.ContainsKey(key))
        {
            return new CustomConfigurationSection(key, _settings[key]);
        }
        return null;
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        foreach (var kvp in _settings)
        {
            yield return new CustomConfigurationSection(kvp.Key, kvp.Value);
        }
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public T GetValue<T>(string key)
    {
        if (_settings.TryGetValue(key, out var value))
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        return default;
    }

    public string this[string key]
    {
        get => _settings.ContainsKey(key) ? _settings[key] : null;
        set => _settings[key] = value;
    }
}

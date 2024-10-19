using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

/// <summary>
/// Represents a custom configuration provider.
/// </summary>
public class Configuration : IConfiguration
{
    private readonly Dictionary<string, string> _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="Configuration"/> class.
    /// </summary>
    /// <param name="settings">The configuration settings.</param>
    public Configuration(Dictionary<string, string> settings)
    {
        _settings = settings;
    }

    /// <inheritdoc />
    public IConfigurationSection GetSection(string key)
    {
        if (_settings.ContainsKey(key))
        {
            return new CustomConfigurationSection(key, _settings[key]);
        }
        return null;
    }

    /// <inheritdoc />
    public IEnumerable<IConfigurationSection> GetChildren()
    {
        foreach (var kvp in _settings)
        {
            yield return new CustomConfigurationSection(kvp.Key, kvp.Value);
        }
    }

    /// <inheritdoc />
    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public T GetValue<T>(string key)
    {
        if (_settings.TryGetValue(key, out var value))
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        return default;
    }

    /// <summary>
    /// Gets or sets the configuration value for the specified key.
    /// </summary>
    /// <param name="key">The configuration key.</param>
    /// <returns>The configuration value.</returns>
    public string this[string key]
    {
        get => _settings.ContainsKey(key) ? _settings[key] : null;
        set => _settings[key] = value;
    }
}

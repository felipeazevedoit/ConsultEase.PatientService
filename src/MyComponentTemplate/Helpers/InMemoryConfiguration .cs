using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

public class InMemoryConfiguration : IConfiguration
{
    private readonly Dictionary<string, string> _settings;

    public InMemoryConfiguration(Dictionary<string, string> settings)
    {
        _settings = settings;
    }

    // Implementa o método GetSection da interface IConfiguration
    public IConfigurationSection GetSection(string key)
    {
        if (_settings.ContainsKey(key))
        {
            return new InMemoryConfigurationSection(key, _settings[key]);
        }
        return null;
    }

    // Implementa o método GetChildren da interface IConfiguration
    public IEnumerable<IConfigurationSection> GetChildren()
    {
        foreach (var kvp in _settings)
        {
            yield return new InMemoryConfigurationSection(kvp.Key, kvp.Value);
        }
    }

    public IChangeToken GetReloadToken()
    {
        // Para esta implementação simples, você pode retornar null ou lançar uma exceção, já que o reload dinâmico não será suportado
        throw new NotImplementedException();
    }

    // Implementa o método GetValue da interface IConfiguration
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

using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

public class InMemoryConfigurationSection : IConfigurationSection
{
    private readonly string _key;
    private readonly string _value;

    public InMemoryConfigurationSection(string key, string value)
    {
        _key = key;
        _value = value;
    }

    public string Key => _key;

    public string Path => _key;

    public string Value
    {
        get => _value;
        set => throw new NotImplementedException();  // No caso dessa implementação, você pode manter como não implementado
    }

    public string this[string key]
    {
        get => throw new NotImplementedException();  // Se não há sub-seções, pode deixar como não implementado
        set => throw new NotImplementedException();
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        // Para esta implementação simples, não há sub-seções
        return new List<IConfigurationSection>();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public IConfigurationSection GetSection(string key)
    {
        throw new NotImplementedException();
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

public class CustomConfigurationSection : IConfigurationSection
{
    private readonly string _key;
    private readonly string _value;

    public CustomConfigurationSection(string key, string value)
    {
        _key = key;
        _value = value;
    }

    public string Key => _key;
    public string Path => _key;
    public string Value
    {
        get => _value;
        set => throw new NotImplementedException();
    }

    public string this[string key]
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        // Retorne seções "filhas", se necessário
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

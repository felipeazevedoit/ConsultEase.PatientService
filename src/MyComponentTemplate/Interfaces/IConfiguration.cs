namespace MyComponentTemplate.Interfaces
{
    public interface IConfiguration
    {
        T GetValue<T>(T v);
    }
}

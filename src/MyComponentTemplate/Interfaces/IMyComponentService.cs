
namespace MyComponentTemplate.Interfaces
{
    public interface IMyComponentService
    {
        Task<IEnumerable<MyComponent>> GetAllComponentsAsync();
        Task<MyComponent> GetComponentByIdAsync(int id);
        Task AddComponentAsync(MyComponent component);
        Task UpdateComponentAsync(MyComponent component);
        Task DeleteComponentAsync(int id);
    }
}

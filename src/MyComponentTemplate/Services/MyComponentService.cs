//using Microsoft.EntityFrameworkCore;
using MyComponentTemplate.Interfaces;
using MyComponentTemplate.Infra.Context;

namespace MyComponentTemplate.Services
{
    public class MyComponentService : IMyComponentService
    {
        private readonly MyComponentDbContext _dbContext;

        public MyComponentService(MyComponentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MyComponent>> GetAllComponentsAsync()
        {
            throw new NotImplementedException();
            //return await _dbContext.MyComponents.ToListAsync();
        }

        public async Task<MyComponent> GetComponentByIdAsync(int id)
        {
            //return await _dbContext.MyComponents.FindAsync(id);
            throw new NotImplementedException();

        }

        public async Task AddComponentAsync(MyComponent component)
        {
            //await _dbContext.MyComponents.AddAsync(component);
            //await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();

        }

        public async Task UpdateComponentAsync(MyComponent component)
        {
            //_dbContext.MyComponents.Update(component);
            //await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();

        }

        public async Task DeleteComponentAsync(int id)
        {
            throw new NotImplementedException();

            //var component = await _dbContext.MyComponents.FindAsync(id);
            //if (component != null)
            //{
            //    _dbContext.MyComponents.Remove(component);
            //    await _dbContext.SaveChangesAsync();
            //}
        }
    }
}

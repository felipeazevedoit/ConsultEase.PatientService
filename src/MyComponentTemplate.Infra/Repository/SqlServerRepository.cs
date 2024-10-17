using Microsoft.EntityFrameworkCore;
using MyComponentTemplate.Infra.Context;

namespace MyComponentTemplate.Infra.Repository
{
    public class SqlServerRepository<T> : IRepository<T> where T : class
    {
        private readonly MyComponentDbContext _context;
        private readonly DbSet<T> _dbSet;

        public SqlServerRepository(MyComponentDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, T entity)
        {
            throw new NotImplementedException();
        }
    }

}

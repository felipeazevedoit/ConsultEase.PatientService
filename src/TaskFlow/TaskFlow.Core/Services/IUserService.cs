namespace TaskFlow.Core.Services
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Get(int id);
        Task<User> GetByEmail(string email);
        Task<List<User>> GetAll();
        Task<User> Update(User user);
        Task<User> Delete(int id);
    }

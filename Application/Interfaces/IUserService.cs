using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);
        Task<List<User>> GetAllUsers();
        Task AddUserAsync(User user);
        Task DeleteUserAsync(string id);
    }
}

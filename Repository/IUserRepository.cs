using WebLibary.Models.Entities;

namespace WebLibary.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidatePasswordAsync(User user, string password);
        Task<int> CreateUserAsync(User user, string password);
    }
}

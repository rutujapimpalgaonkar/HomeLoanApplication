using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<int> AddUserAsync(User user);
        Task<bool> UserExistsAsync(int userId);
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string name);
        Task GetUserByEmailAsync(string email);
        Task<User> GetByNameAsync(object applicationId);
        Task<User> GetByEmailAsync(string email);
    }
}

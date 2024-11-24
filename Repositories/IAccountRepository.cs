using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> AccountExistsAsync(int accountId);
    }
}

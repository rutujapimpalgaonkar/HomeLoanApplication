using HomeLoanApplication.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HomeLoanContext _context;

        public AccountRepository(HomeLoanContext context)
        {
            _context = context;
        }

        public async Task<bool> AccountExistsAsync(int accountId)
        {
            return await _context.Accounts.AnyAsync(a => a.AccountId == accountId);
        }
    }
}

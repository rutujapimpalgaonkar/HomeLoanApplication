using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface ILoanTrackerRepository
    {
        Task<LoanTracker> GetLoanTrackerByIdAsync(int id);
        Task<int> AddLoanTrackerAsync(LoanTracker loanTracker);
        Task<bool> UpdateLoanTrackerStatusAsync(int id, string newStatus);
        Task<bool> DeleteLoanTrackerAsync(int id);
    }
}

using HomeLoanApplication.DTOs;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public interface ILoanTrackerService
    {
        Task<LoanTrackerDTO> GetLoanTrackerByIdAsync(int id);
        Task<int> AddLoanTrackerAsync(LoanTrackerDTO loanTrackerDTO);
        Task<bool> UpdateLoanTrackerStatusAsync(int id, string newStatus);
        Task<bool> DeleteLoanTrackerAsync(int id);
    }
}

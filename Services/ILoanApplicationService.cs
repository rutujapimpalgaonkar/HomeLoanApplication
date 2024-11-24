// ILoanApplicationService.cs
using HomeLoanApplication.DTOs;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public interface ILoanApplicationService
    {
        Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(int id);
        Task<int> AddLoanApplicationAsync(LoanApplicationDTO loanApplicationDTO);
        Task<LoanApplicationDTO> UpdateLoanApplicationAsync(int id, LoanApplicationDTO loanApplicationDTO);
        Task<bool> DeleteLoanApplicationAsync(int id);
    }
}

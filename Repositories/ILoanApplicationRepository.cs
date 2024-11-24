// ILoanApplicationRepository.cs
using HomeLoanApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface ILoanApplicationRepository
    {
        Task<LoanApplication> GetLoanApplicationByIdAsync(int id);
        Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync();
        Task<LoanApplication> AddLoanApplicationAsync(LoanApplication loanApplication);
        Task<LoanApplication> UpdateLoanApplicationAsync(int id, LoanApplication loanApplication);
        Task<bool> DeleteLoanApplicationAsync(int id);
    }
}

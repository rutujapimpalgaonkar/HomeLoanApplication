using HomeLoanApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface IIncomeDetailRepository
    {
        Task<IncomeDetail> GetIncomeDetailByIdAsync(int id);
        Task<IEnumerable<IncomeDetail>> GetAllIncomeDetailsAsync();
        Task<IncomeDetail> AddIncomeDetailAsync(IncomeDetail incomeDetail);
        Task<IncomeDetail> UpdateIncomeDetailAsync(IncomeDetail incomeDetail);
        Task<bool> DeleteIncomeDetailAsync(int id);
        Task<bool> ValidateApplicationIdExistsAsync(int applicationId);
    }
}

using HomeLoanApplication.DTOs;
using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public interface IIncomeDetailService
    {
        // Get IncomeDetail by ID and return as DTO
        Task<IncomeDetailDTO> GetIncomeDetailByIdAsync(int id);
        Task<IEnumerable<IncomeDetailDTO>> GetIncomeDetailsAsync();
        // Add IncomeDetail from DTO and return the IncomeDetailId
        Task<int> AddIncomeDetailAsync(IncomeDetailDTO incomeDetailDTO);  
        
        // Validate if ApplicationId exists in the system
        Task<bool> ValidateApplicationIdExistsAsync(int applicationId);
        // Task UpdateIncomeDetailAsync(int id, IncomeDetailDTO incomeDetailDTO);
        Task<bool> DeleteIncomeDetailAsync(int id);
         Task<IncomeDetailDTO> UpdateIncomeDetailAsync(int id, IncomeDetailDTO incomeDetailDTO);  // Return IncomeDetailDTO
    }

}

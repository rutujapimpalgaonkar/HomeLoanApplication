using HomeLoanApplication.DTOs;
using HomeLoanApplication.Repositories;
using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class IncomeDetailService : IIncomeDetailService
    {
        private readonly IIncomeDetailRepository _incomeDetailRepository;

        public IncomeDetailService(IIncomeDetailRepository incomeDetailRepository)
        {
            _incomeDetailRepository = incomeDetailRepository;
        }

        // Get IncomeDetail by ID
        public async Task<IncomeDetailDTO> GetIncomeDetailByIdAsync(int id)
        {
            var incomeDetail = await _incomeDetailRepository.GetIncomeDetailByIdAsync(id);
            if (incomeDetail == null)
            {
                return null;
            }

            // Convert to DTO and return
            return new IncomeDetailDTO
            {
                IncomeDetailId = incomeDetail.IncomeDetailId,
                NetSalary = incomeDetail.NetSalary,
                EmploymentType = incomeDetail.EmploymentType,
                EmployerName = incomeDetail.EmployerName,
                ApplicationId = incomeDetail.ApplicationId
            };
        }

        // Add a new IncomeDetail
        public async Task<int> AddIncomeDetailAsync(IncomeDetailDTO incomeDetailDTO)
        {
            // Convert DTO to entity
            var incomeDetail = new IncomeDetail
            {
                NetSalary = incomeDetailDTO.NetSalary,
                EmploymentType = incomeDetailDTO.EmploymentType,
                EmployerName = incomeDetailDTO.EmployerName,
                ApplicationId = incomeDetailDTO.ApplicationId
            };

            var addedIncomeDetail = await _incomeDetailRepository.AddIncomeDetailAsync(incomeDetail);
            return addedIncomeDetail.IncomeDetailId;
        }

        // Update an existing IncomeDetail
        public async Task<IncomeDetailDTO> UpdateIncomeDetailAsync(int id, IncomeDetailDTO incomeDetailDTO)
        {
            // Get existing IncomeDetail
            var existingIncomeDetail = await _incomeDetailRepository.GetIncomeDetailByIdAsync(id);
            if (existingIncomeDetail == null)
            {
                return null;
            }

            // Update the existing entity
            existingIncomeDetail.NetSalary = incomeDetailDTO.NetSalary;
            existingIncomeDetail.EmploymentType = incomeDetailDTO.EmploymentType;
            existingIncomeDetail.EmployerName = incomeDetailDTO.EmployerName;
            existingIncomeDetail.ApplicationId = incomeDetailDTO.ApplicationId;

            // Save changes
            var updatedIncomeDetail = await _incomeDetailRepository.UpdateIncomeDetailAsync(existingIncomeDetail);

            // Return the updated DTO
            return new IncomeDetailDTO
            {
                IncomeDetailId = updatedIncomeDetail.IncomeDetailId,
                NetSalary = updatedIncomeDetail.NetSalary,
                EmploymentType = updatedIncomeDetail.EmploymentType,
                EmployerName = updatedIncomeDetail.EmployerName,
                ApplicationId = updatedIncomeDetail.ApplicationId
            };
        }

        // Delete an IncomeDetail
        public async Task<bool> DeleteIncomeDetailAsync(int id)
        {
            return await _incomeDetailRepository.DeleteIncomeDetailAsync(id);
        }

        // Validate if ApplicationId exists
        public async Task<bool> ValidateApplicationIdExistsAsync(int applicationId)
        {
            return await _incomeDetailRepository.ValidateApplicationIdExistsAsync(applicationId);
        }

        public Task<IEnumerable<IncomeDetailDTO>> GetIncomeDetailsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

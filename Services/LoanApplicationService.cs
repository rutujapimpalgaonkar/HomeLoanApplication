// LoanApplicationService.cs
using HomeLoanApplication.DTOs;
using HomeLoanApplication.Models;
using HomeLoanApplication.Repositories;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public LoanApplicationService(ILoanApplicationRepository loanApplicationRepository)
        {
            _loanApplicationRepository = loanApplicationRepository;
        }

        // Get LoanApplication by ID
        public async Task<LoanApplicationDTO> GetLoanApplicationByIdAsync(int id)
        {
            var loanApplication = await _loanApplicationRepository.GetLoanApplicationByIdAsync(id);
            if (loanApplication == null)
            {
                return null;
            }

            // Convert LoanApplication to LoanApplicationDTO
            return new LoanApplicationDTO
            {
                ApplicationId = loanApplication.ApplicationId,
                UserId = loanApplication.UserId,
                LoanAmountRequested = loanApplication.LoanAmountRequested,
                MonthlyIncome = loanApplication.MonthlyIncome,
                Tenure = loanApplication.Tenure,
                PropertyLocation = loanApplication.PropertyLocation,
                PropertyName = loanApplication.PropertyName,
                EstimatedPropertyCost = loanApplication.EstimatedPropertyCost,
                ApplicationStatus = loanApplication.ApplicationStatus
            };
        }

        // Add LoanApplication
        public async Task<int> AddLoanApplicationAsync(LoanApplicationDTO loanApplicationDTO)
        {
            // Convert LoanApplicationDTO to LoanApplication entity
            var loanApplication = new LoanApplication
            {
                UserId = loanApplicationDTO.UserId,
                LoanAmountRequested = loanApplicationDTO.LoanAmountRequested,
                MonthlyIncome = loanApplicationDTO.MonthlyIncome,
                Tenure = loanApplicationDTO.Tenure,
                PropertyLocation = loanApplicationDTO.PropertyLocation,
                PropertyName = loanApplicationDTO.PropertyName,
                EstimatedPropertyCost = loanApplicationDTO.EstimatedPropertyCost,
                ApplicationStatus = loanApplicationDTO.ApplicationStatus
            };

            // Add to the repository
            var addedLoanApplication = await _loanApplicationRepository.AddLoanApplicationAsync(loanApplication);

            // Return the LoanApplicationId of the added record
            return addedLoanApplication.ApplicationId;
        }

        // Update LoanApplication
        public async Task<LoanApplicationDTO> UpdateLoanApplicationAsync(int id, LoanApplicationDTO loanApplicationDTO)
        {
            // Convert LoanApplicationDTO to LoanApplication entity
            var loanApplication = new LoanApplication
            {
                ApplicationId = id,
                UserId = loanApplicationDTO.UserId,
                LoanAmountRequested = loanApplicationDTO.LoanAmountRequested,
                MonthlyIncome = loanApplicationDTO.MonthlyIncome,
                Tenure = loanApplicationDTO.Tenure,
                PropertyLocation = loanApplicationDTO.PropertyLocation,
                PropertyName = loanApplicationDTO.PropertyName,
                EstimatedPropertyCost = loanApplicationDTO.EstimatedPropertyCost,
                ApplicationStatus = loanApplicationDTO.ApplicationStatus
            };

            // Update the repository
            var updatedLoanApplication = await _loanApplicationRepository.UpdateLoanApplicationAsync(id, loanApplication);

            // Return updated LoanApplicationDTO
            return new LoanApplicationDTO
            {
                ApplicationId = updatedLoanApplication.ApplicationId,
                UserId = updatedLoanApplication.UserId,
                LoanAmountRequested = updatedLoanApplication.LoanAmountRequested,
                MonthlyIncome = updatedLoanApplication.MonthlyIncome,
                Tenure = updatedLoanApplication.Tenure,
                PropertyLocation = updatedLoanApplication.PropertyLocation,
                PropertyName = updatedLoanApplication.PropertyName,
                EstimatedPropertyCost = updatedLoanApplication.EstimatedPropertyCost,
                ApplicationStatus = updatedLoanApplication.ApplicationStatus
            };
        }

        // Delete LoanApplication by ID
        public async Task<bool> DeleteLoanApplicationAsync(int id)
        {
            return await _loanApplicationRepository.DeleteLoanApplicationAsync(id);
        }
    }
}

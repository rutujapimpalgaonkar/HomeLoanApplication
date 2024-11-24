using HomeLoanApplication.DTOs;
using HomeLoanApplication.Models;
using HomeLoanApplication.Repositories;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class LoanTrackerService : ILoanTrackerService
    {
        private readonly ILoanTrackerRepository _loanTrackerRepository;
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public LoanTrackerService(ILoanTrackerRepository loanTrackerRepository, ILoanApplicationRepository loanApplicationRepository)
        {
            _loanTrackerRepository = loanTrackerRepository;
            _loanApplicationRepository = loanApplicationRepository;
        }

        public async Task<LoanTrackerDTO> GetLoanTrackerByIdAsync(int id)
        {
            var loanTracker = await _loanTrackerRepository.GetLoanTrackerByIdAsync(id);
            if (loanTracker == null)
            {
                return null;
            }

            return new LoanTrackerDTO
            {
                TrackerId = loanTracker.TrackerId,
                ApplicationId = (int)loanTracker.ApplicationId,
                Status = loanTracker.Status
            };
        }

        public async Task<int> AddLoanTrackerAsync(LoanTrackerDTO loanTrackerDTO)
        {
            var loanApplication = await _loanApplicationRepository.GetLoanApplicationByIdAsync(loanTrackerDTO.ApplicationId);
            if (loanApplication == null)
            {
                throw new ArgumentException($"Loan Application with ApplicationId {loanTrackerDTO.ApplicationId} does not exist.");
            }

            var loanTracker = new LoanTracker
            {
                ApplicationId = loanTrackerDTO.ApplicationId,
                Status = loanTrackerDTO.Status
            };

            return await _loanTrackerRepository.AddLoanTrackerAsync(loanTracker);
        }

        public async Task<bool> UpdateLoanTrackerStatusAsync(int id, string newStatus)
        {
            return await _loanTrackerRepository.UpdateLoanTrackerStatusAsync(id, newStatus);
        }

        public async Task<bool> DeleteLoanTrackerAsync(int id)
        {
            return await _loanTrackerRepository.DeleteLoanTrackerAsync(id);
        }
    }
}

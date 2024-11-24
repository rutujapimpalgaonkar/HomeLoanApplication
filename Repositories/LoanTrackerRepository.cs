using HomeLoanApplication.Data;
using HomeLoanApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public class LoanTrackerRepository : ILoanTrackerRepository
    {
        private readonly HomeLoanContext _context;

        public LoanTrackerRepository(HomeLoanContext context)
        {
            _context = context;
        }

        public async Task<LoanTracker> GetLoanTrackerByIdAsync(int id)
        {
            return await _context.LoanTrackers
                                 .FirstOrDefaultAsync(lt => lt.TrackerId == id);
        }

        public async Task<int> AddLoanTrackerAsync(LoanTracker loanTracker)
        {
            _context.LoanTrackers.Add(loanTracker);
            await _context.SaveChangesAsync();
            return loanTracker.TrackerId;
        }

        public async Task<bool> UpdateLoanTrackerStatusAsync(int id, string newStatus)
        {
            var loanTracker = await _context.LoanTrackers.FindAsync(id);
            if (loanTracker == null) return false;

            loanTracker.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLoanTrackerAsync(int id)
        {
            var loanTracker = await _context.LoanTrackers.FindAsync(id);
            if (loanTracker == null) return false;

            _context.LoanTrackers.Remove(loanTracker);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

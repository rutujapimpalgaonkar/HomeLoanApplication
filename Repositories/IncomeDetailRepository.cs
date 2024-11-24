using HomeLoanApplication.Data;
using HomeLoanApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public class IncomeDetailRepository : IIncomeDetailRepository
    {
        private readonly HomeLoanContext _context;

        public IncomeDetailRepository(HomeLoanContext context)
        {
            _context = context;
        }

        public async Task<IncomeDetail> GetIncomeDetailByIdAsync(int id)
        {
            return await _context.IncomeDetails.FirstOrDefaultAsync(i => i.IncomeDetailId == id);
        }

        public async Task<IEnumerable<IncomeDetail>> GetAllIncomeDetailsAsync()
        {
            return await _context.IncomeDetails.ToListAsync();
        }

        public async Task<IncomeDetail> AddIncomeDetailAsync(IncomeDetail incomeDetail)
        {
            await _context.IncomeDetails.AddAsync(incomeDetail);
            await _context.SaveChangesAsync();
            return incomeDetail;
        }

        public async Task<IncomeDetail> UpdateIncomeDetailAsync(IncomeDetail incomeDetail)
        {
            _context.IncomeDetails.Update(incomeDetail);
            await _context.SaveChangesAsync();
            return incomeDetail;
        }

        public async Task<bool> DeleteIncomeDetailAsync(int id)
        {
            var incomeDetail = await _context.IncomeDetails.FindAsync(id);
            if (incomeDetail == null)
            {
                return false;
            }

            _context.IncomeDetails.Remove(incomeDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateApplicationIdExistsAsync(int applicationId)
        {
            return await _context.LoanApplications.AnyAsync(l => l.ApplicationId == applicationId);
        }
    }
}

using HomeLoanApplication.Data;
using HomeLoanApplication.Models;
using HomeLoanApplication.Repositories;
using Microsoft.EntityFrameworkCore;

public class LoanApplicationRepository : ILoanApplicationRepository
{
    private readonly HomeLoanContext _context;

    public LoanApplicationRepository(HomeLoanContext context)
    {
        _context = context;
    }

    public async Task<LoanApplication> AddLoanApplicationAsync(LoanApplication loanApplication)
    {
        _context.LoanApplications.Add(loanApplication);
        await _context.SaveChangesAsync();
        return loanApplication;
    }

    public async Task<LoanApplication> GetLoanApplicationByIdAsync(int id)
    {
        return await _context.LoanApplications
            .Include(l => l.User) // You can include other related entities as needed
            .FirstOrDefaultAsync(l => l.ApplicationId == id);
    }

    public async Task<bool> DeleteLoanApplicationAsync(int id)
    {
        var loanApplication = await _context.LoanApplications.FindAsync(id);
        if (loanApplication == null)
        {
            return false;
        }

        _context.LoanApplications.Remove(loanApplication);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<LoanApplication> UpdateLoanApplicationAsync(int id, LoanApplication loanApplication)
    {
        var existingLoanApplication = await _context.LoanApplications.FindAsync(id);
        if (existingLoanApplication == null)
        {
            return null;
        }

        existingLoanApplication.LoanAmountRequested = loanApplication.LoanAmountRequested;
        existingLoanApplication.MonthlyIncome = loanApplication.MonthlyIncome;
        existingLoanApplication.Tenure = loanApplication.Tenure;
        existingLoanApplication.PropertyLocation = loanApplication.PropertyLocation;
        existingLoanApplication.PropertyName = loanApplication.PropertyName;
        existingLoanApplication.EstimatedPropertyCost = loanApplication.EstimatedPropertyCost;
        existingLoanApplication.ApplicationStatus = loanApplication.ApplicationStatus;

        await _context.SaveChangesAsync();
        return existingLoanApplication;
    }

    public Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync()
    {
        throw new NotImplementedException();
    }
}

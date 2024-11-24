// Models/IncomeDetail.cs
namespace HomeLoanApplication.Models
{
    public class IncomeDetail
    {
        public int IncomeDetailId { get; set; }
        public int ApplicationId { get; set; }
        public decimal NetSalary { get; set; }
        public string EmploymentType { get; set; }
        public string EmployerName { get; set; }

        // Navigation property for related LoanApplication (if needed)
        public LoanApplication LoanApplication { get; set; }
    }
}

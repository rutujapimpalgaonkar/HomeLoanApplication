using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.Models
{
    public class LoanApplication
    {
        [Key]
        public int ApplicationId { get; set; }  // Primary Key

        public int UserId { get; set; }  // Foreign Key referencing User (user_id)

        public decimal LoanAmountRequested { get; set; }  // Loan Amount Requested (loan_amount_requested)

        public decimal MonthlyIncome { get; set; }  // Monthly Income (monthly_income)

        public int Tenure { get; set; }  // Tenure (tenure)

        public string PropertyLocation { get; set; }  // Property Location (property_location)

        public string PropertyName { get; set; }  // Property Name (property_name)

        public decimal EstimatedPropertyCost { get; set; }  // Estimated Property Cost (estimated_property_cost)

        public string ApplicationStatus { get; set; }  // Application Status (application_status)

        // Navigation property for User
        public User User { get; set; }

        // Navigation properties for related Documents, IncomeDetails, and Account
        public ICollection<Document> Documents { get; set; }
        public ICollection<IncomeDetail> IncomeDetails { get; set; }
        public Account Account { get; set; }
        public LoanTracker LoanTracker { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }  // Primary Key
        public int? ApplicationId { get; set; }  // Foreign Key reference to LoanApplication
        public decimal Balance { get; set; }  // Balance

        // Navigation property for the related LoanApplication
        public LoanApplication LoanApplication { get; set; }
    }
}

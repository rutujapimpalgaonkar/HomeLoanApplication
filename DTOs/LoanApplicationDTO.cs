// DTOs/LoanApplicationDTO.cs
using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.DTOs
{
    public class LoanApplicationDTO
    {
        // public int ApplicationId { get; set; }  // Primary Key
        // public int UserId { get; set; }  // Foreign Key to User (user_id)
        // public decimal LoanAmountRequested { get; set; }  // Loan Amount Requested
        // public decimal MonthlyIncome { get; set; }  // Monthly Income
        // public int Tenure { get; set; }  // Tenure
        // public string PropertyLocation { get; set; }  // Property Location
        // public string PropertyName { get; set; }  // Property Name
        // public decimal EstimatedPropertyCost { get; set; }  // Estimated Property Cost
        // public string ApplicationStatus { get; set; }  // Application Status

        // Primary Key
        [Required(ErrorMessage = "ApplicationId is required.")]
        public int ApplicationId { get; set; }

        // Foreign Key to User
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        // Loan Amount Requested
        [Required(ErrorMessage = "Loan Amount Requested is required.")]
        [Range(1000, 5000000, ErrorMessage = "Loan Amount Requested must be between 1000 and 5,000,000.")]
        public decimal LoanAmountRequested { get; set; }

        // Monthly Income
        [Required(ErrorMessage = "Monthly Income is required.")]
        [Range(1000, 1000000, ErrorMessage = "Monthly Income must be between 1000 and 1,000,000.")]
        public decimal MonthlyIncome { get; set; }

        // Tenure (in months)
        [Required(ErrorMessage = "Tenure is required.")]
        [Range(12, 360, ErrorMessage = "Tenure must be between 12 and 360 months.")]
        public int Tenure { get; set; }

        // Property Location
        [Required(ErrorMessage = "Property Location is required.")]
        [StringLength(200, ErrorMessage = "Property Location cannot be longer than 200 characters.")]
        public string PropertyLocation { get; set; }

        // Property Name
        [Required(ErrorMessage = "Property Name is required.")]
        [StringLength(100, ErrorMessage = "Property Name cannot be longer than 100 characters.")]
        public string PropertyName { get; set; }

        // Estimated Property Cost
        [Required(ErrorMessage = "Estimated Property Cost is required.")]
        [Range(10000, 10000000, ErrorMessage = "Estimated Property Cost must be between 10,000 and 10,000,000.")]
        public decimal EstimatedPropertyCost { get; set; }

        // Application Status (e.g., Pending, Approved, Denied)
        [Required(ErrorMessage = "Application Status is required.")]
        [StringLength(50, ErrorMessage = "Application Status cannot be longer than 50 characters.")]
        [RegularExpression(@"^(Pending|Approved|Denied)$", ErrorMessage = "Application Status must be either 'Pending', 'Approved', or 'Denied'.")]
        public string ApplicationStatus { get; set; }
    }
}

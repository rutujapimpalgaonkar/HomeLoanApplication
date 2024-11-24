// DTOs/IncomeDetailDTO.cs
using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.DTOs
{
    public class IncomeDetailDTO
    {
        // public int IncomeDetailId { get; set; }
        // public int ApplicationId { get; set; }
        // public decimal NetSalary { get; set; }
        // public string EmploymentType { get; set; }
        // public string EmployerName { get; set; }

         // Income Detail Id - Primary Key
        [Required(ErrorMessage = "IncomeDetailId is required.")]
        public int IncomeDetailId { get; set; }

        // Foreign Key to Loan Application
        [Required(ErrorMessage = "ApplicationId is required.")]
        public int ApplicationId { get; set; }

        // Net Salary - The salary after deductions
        [Required(ErrorMessage = "Net Salary is required.")]
        [Range(1000, 5000000, ErrorMessage = "Net Salary must be between 1000 and 5,000,000.")]
        public decimal NetSalary { get; set; }

        // Employment Type (e.g., Full-time, Part-time, Self-employed)
        [Required(ErrorMessage = "Employment Type is required.")]
        [StringLength(50, ErrorMessage = "Employment Type cannot be longer than 50 characters.")]
        public string EmploymentType { get; set; }

        // Employer Name
        [Required(ErrorMessage = "Employer Name is required.")]
        [StringLength(100, ErrorMessage = "Employer Name cannot be longer than 100 characters.")]
        public string EmployerName { get; set; }

    }
}

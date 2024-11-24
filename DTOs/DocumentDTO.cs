using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.DTOs
{
    public class DocumentDTO
    {
        // public int DocumentId { get; set; }
        // public int ApplicationId { get; set; }  // Foreign Key to LoanApplication
        // public string DocumentType { get; set; }
        // public string DocumentUrl { get; set; }
        // public string? UserId { get; internal set; }


        // Primary Key
        [Required(ErrorMessage = "DocumentId is required.")]
        public int DocumentId { get; set; }

        // Foreign Key to LoanApplication
        [Required(ErrorMessage = "ApplicationId is required.")]
        public int ApplicationId { get; set; }

        // Document Type (e.g., 'IdentityProof', 'IncomeProof', etc.)
        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(50, ErrorMessage = "Document Type cannot be longer than 50 characters.")]
        public string DocumentType { get; set; }

        // URL of the document (where the document is stored)
        [Required(ErrorMessage = "Document URL is required.")]
        [Url(ErrorMessage = "Document URL must be a valid URL.")]
        public string DocumentUrl { get; set; }

        // Optional: UserId who uploaded the document (can be null)
        public string? UserId { get; internal set; }
    }
}

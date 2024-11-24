using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.Models
{
    public class Document
{
    public int DocumentId { get; set; }
    
    // Make ApplicationId nullable
    public int? ApplicationId { get; set; }  // Nullable foreign key

    public string DocumentType { get; set; }
    public string DocumentUrl { get; set; }

    public LoanApplication LoanApplication { get; set; }  // Navigation property
}


}

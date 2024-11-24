using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HomeLoanApplication.Models
{
    public class LoanTracker
    {
        [Key]
        public int TrackerId { get; set; }  // Primary Key (tracker_id)
        public int? ApplicationId { get; set; }  // Foreign Key (application_id)
        public string Status { get; set; }  // Status (status)

        // Navigation property to LoanApplication
        public LoanApplication LoanApplication { get; set; }
    }
}

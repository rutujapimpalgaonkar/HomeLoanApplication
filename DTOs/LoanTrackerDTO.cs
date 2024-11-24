using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.DTOs
{
    public class LoanTrackerDTO
    {
        // public int TrackerId { get; set; }
        // public int ApplicationId { get; set; }
        // public string Status { get; set; }
        // // public string? UserId { get; internal set; }

        [Required(ErrorMessage = "TrackerId is required.")]
        public int TrackerId { get; set; }

        [Required(ErrorMessage = "ApplicationId is required.")]
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        [RegularExpression(@"^(Pending|Approved|Denied)$", ErrorMessage = "Status must be either 'Pending', 'Approved', or 'Denied'.")]
        public string Status { get; set; }
    }
}

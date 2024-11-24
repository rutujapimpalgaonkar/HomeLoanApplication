using System.ComponentModel.DataAnnotations;

namespace HomeLoanApplication.DTOs
{
    public class LoginDTO
    {
        // public string Email { get; set; }  // Use Email for login
        // public string Password { get; set; }
       


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }  // Use Email for login

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}

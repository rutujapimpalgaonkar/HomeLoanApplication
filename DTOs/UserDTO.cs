using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password should be at least 6 characters long.")]
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    // public string Password { get; set; }  // Make sure this field is included
    public int UserId { get; internal set; }
}

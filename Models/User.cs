public class User
{
    public static bool IsAdmin { get; internal set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }  // Ensure this field is also here
}

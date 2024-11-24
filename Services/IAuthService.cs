using HomeLoanApplication.DTOs;


namespace HomeLoanApplication.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginDTO loginDto);  // This method returns a JWT token
         Task<string> LoginAsync(LoginDTO loginDTO);
    }
} 
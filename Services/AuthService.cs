// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using HomeLoanApplication.DTOs;
// using HomeLoanApplication.Models;
// using HomeLoanApplication.Repositories;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using System.Security.Claims;
// using System;

// namespace HomeLoanApplication.Services
// {
//     public class AuthService : IAuthService
//     {
//         private readonly IUserRepository _userRepository;
//         private readonly string _jwtSecretKey;  // Secret key for JWT token
//         private readonly string _jwtIssuer;     // JWT Issuer
//         private readonly string _jwtAudience;   // JWT Audience

//         public AuthService(IUserRepository userRepository, IConfiguration configuration)
//         {
//             _userRepository = userRepository;
//             // Securely store secret key, issuer, and audience in appsettings.json
//             _jwtSecretKey = configuration["JwtSettings:SecretKey"];
//             _jwtIssuer = configuration["JwtSettings:Issuer"];
//             _jwtAudience = configuration["JwtSettings:Audience"];
//         }

//         // Asynchronous method for authenticating user
//         public async Task<string> Authenticate(LoginDTO loginDTO)
//         {
//             // Retrieve the user by Email (or ApplicationId if needed)
//             var user = await _userRepository.GetByEmailAsync(loginDTO.Email);  // Adjusted to use Email

//             if (user == null)
//             {
//                 return null;  // Invalid credentials (user not found)
//             }

//             // Verify the user's password using PasswordHasher
//             var passwordHasher = new PasswordHasher<User>();
//             var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);

//             if (verificationResult == PasswordVerificationResult.Failed)
//             {
//                 return null;  // Invalid credentials (password mismatch)
//             }

//             // Generate the JWT token after successful login
//             return GenerateJwtToken(user);  // Use the user object to generate JWT
//         }

//         public Task<string> AuthenticateAsync(LoginDTO loginDto)
//         {
//             throw new NotImplementedException();
//         }

//         public Task<string> LoginAsync(LoginDTO loginDTO)
//         {
//             throw new NotImplementedException();
//         }

//         // Method to generate JWT token after successful login
//         private string GenerateJwtToken(User user)
//         {
//             // Define claims for the JWT token (UserId, Name, Email, etc.)
//             var claims = new[]
//             {
//                 new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),   // UserId as Subject
//                 new Claim(ClaimTypes.Name, user.Name),                            // User name
//                 new Claim(ClaimTypes.Email, user.Email),                          // User email
//                 new Claim(ClaimTypes.Role, user.Role),                             // User role
//                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT Token ID (unique)
//             };

//             // Define the security key using the secret key from appsettings
//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             // Create the JWT token
//             var token = new JwtSecurityToken(
//                 issuer: _jwtIssuer,  // Issuer of the token
//                 audience: _jwtAudience,  // Audience of the token
//                 claims: claims,  // Claims for user
//                 expires: DateTime.Now.AddHours(1),  // Token expiration time (1 hour)
//                 signingCredentials: creds  // Signing credentials for the token
//             );

//             // Return the JWT token as a string
//             return new JwtSecurityTokenHandler().WriteToken(token);
//         }
//     }
// }


// AuthService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HomeLoanApplication.DTOs;
using HomeLoanApplication.Repositories;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _jwtSecretKey;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _jwtSecretKey = configuration["JwtSettings:SecretKey"];
        _jwtIssuer = configuration["JwtSettings:Issuer"];
        _jwtAudience = configuration["JwtSettings:Audience"];
    }

    // Implement AuthenticateAsync method
    public async Task<string> AuthenticateAsync(LoginDTO loginDTO)
    {
        // Retrieve the user by ApplicationId (Email or Username)
        var user = await _userRepository.GetByEmailAsync(loginDTO.Email);  // Using Email as identifier
        
        if (user == null)
        {
            return null;  // Return null if user is not found
        }

        // Verify the user's password using PasswordHasher (if using hashed passwords)
        var passwordHasher = new PasswordHasher<User>();  // Password hash verification
        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return null;  // Invalid credentials
        }

        // If authentication is successful, generate the JWT token
        return GenerateJwtToken(user); // Generate JWT token and return
    }

    public Task<string> LoginAsync(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    // Method to generate JWT token
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),   // UserId as Subject
            new Claim(ClaimTypes.Name, user.Name),                            // User name
            new Claim(ClaimTypes.Email, user.Email),                          // User email
            new Claim(ClaimTypes.Role, user.Role),                             // User role
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT Token ID (unique)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey)); 
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,  
            audience: _jwtAudience,  
            claims: claims,  
            expires: DateTime.Now.AddDays(1),  
            signingCredentials: creds  
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/login (Login with ApplicationId and Password)
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Authenticate the user and generate the JWT token
            var token = await _authService.AuthenticateAsync(loginDto); // Corrected to Authenticate, not AuthenticateAsync

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid ApplicationId or Password");
            }

            // Return the JWT token
            return Ok(new { Token = token });
        }
    }
}


// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.Extensions.Configuration;

// namespace YourNamespace.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class AuthController : ControllerBase
//     {
//         private readonly IConfiguration _configuration;

//         // Constructor to inject IConfiguration
//         public AuthController(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         // POST /api/auth/login
//         [HttpPost("login")]
//         public IActionResult Login([FromBody] dynamic data)
//         {
//             // Extracting data directly from the request body
//             string username = data?.Username;
//             string password = data?.Password;
//             string applicationId = data?.ApplicationId;

//             // Validate ApplicationId (if required)
//             if (string.IsNullOrEmpty(applicationId))
//             {
//                 return BadRequest(new { message = "ApplicationId is required." });
//             }

//             // Validate the user credentials (this is a simple check, ideally, use a real authentication method)
//             if (username != "admin" || password != "password")
//             {
//                 return Unauthorized(); // Invalid credentials
//             }

//             // Generate JWT token
//             var token = GenerateJwtToken(username);

//             return Ok(new { token }); // Return token to the client
//         }

//         // Method to generate the JWT token
//         private string GenerateJwtToken(string username)
//         {
//             var claims = new[]
//             {
//                 new Claim(ClaimTypes.Name, username),
//                 new Claim(ClaimTypes.Role, "Admin") // You can add more claims if needed
//             };

//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var token = new JwtSecurityToken(
//                 issuer: _configuration["JwtSettings:Issuer"],
//                 audience: _configuration["JwtSettings:Audience"],
//                 claims: claims,
//                 expires: DateTime.Now.AddHours(1), // Token expires in 1 hour
//                 signingCredentials: creds
//             );

//             return new JwtSecurityTokenHandler().WriteToken(token);
//         }
//     }
// }

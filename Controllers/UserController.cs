// using HomeLoanApplication.DTOs;
// using HomeLoanApplication.Services;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;

// namespace HomeLoanApplication.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UserController : ControllerBase
//     {
//         private readonly IUserService _userService;

//         public UserController(IUserService userService)
//         {
//             _userService = userService;
//         }

//         // POST: api/user/AdminorUser (Add User)
//         [HttpPost("AdminorUser")]
//         [Authorize(Roles = "admin, user")]
//         public async Task<ActionResult> AddUser([FromBody] UserDTO userDTO)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             await _userService.AddUserAsync(userDTO);
//             return Ok("User added successfully");
//         }

//         // GET: api/user/{id} (Get User by ID)
//         [HttpGet("{id}")]
//         [Authorize(Roles = "admin")]
//         public async Task<ActionResult<UserDTO>> GetUser(int id)
//         {
//             var userDTO = await _userService.GetUserByIdAsync(id);

//             if (userDTO == null)
//             {
//                 return NotFound("User not found.");
//             }

//             return Ok(userDTO); // Return 200 OK with UserDTO
//         }

//         // GET: api/users/byname/{name} (Get User by Name)
//         [HttpGet("byname/{name}")]
//         public async Task<ActionResult<UserDTO>> GetUserByName(string name)
//         {
//             var userDTO = await _userService.GetUserByNameAsync(name);

//             if (userDTO == null)
//             {
//                 return NotFound("User not found.");
//             }

//             return Ok(userDTO); // Return 200 OK with UserDTO
//         }
//     }
// }

using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user/AdminorUser (Add User)
        [HttpPost("AdminorUser")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult> AddUser([FromBody] UserDTO userDTO)
        {
            // Validate the incoming UserDTO using data annotations
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            // Proceed with adding the user if validation passes
            await _userService.AddUserAsync(userDTO);
            return Ok("User added successfully");
        }

        // GET: api/user/{id} (Get User by ID)
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            // Retrieve user by ID
            var userDTO = await _userService.GetUserByIdAsync(id);

            if (userDTO == null)
            {
                return NotFound("User not found.");
            }

            return Ok(userDTO); // Return 200 OK with UserDTO
        }

        // GET: api/users/byname/{name} (Get User by Name)
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string name)
        {
            // Retrieve user by name
            var userDTO = await _userService.GetUserByNameAsync(name);

            if (userDTO == null)
            {
                return NotFound("User not found.");
            }

            return Ok(userDTO); // Return 200 OK with UserDTO
        }
    }
}


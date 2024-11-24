using HomeLoanApplication.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class UserService : IUserService // Implements IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> AddUserAsync(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                Role = userDTO.Role,
                IsActive = userDTO.IsActive,
                Password = userDTO.Password // Directly using password from DTO (later we will hash it)
            };

            // Hash the password before saving it
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, userDTO.Password);

            await _userRepository.AddUserAsync(user);  // Ensure the repository method is called
            return user.UserId;
        }

        // Implement the GetUserByIdAsync method
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return userDTO;
        }

        // New method to get a user by name
        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetByNameAsync(name); // Call repository method to get by name

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }

        // Implement UserExistsAsync method
        public async Task<bool> UserExistsAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }
    }
}

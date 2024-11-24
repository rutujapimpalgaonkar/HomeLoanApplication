public interface IUserService
{
    Task<UserDTO> GetUserByIdAsync(int id); // Should return UserDTO
    Task<User> GetUserByNameAsync(string name);
    Task<int> AddUserAsync(UserDTO userDTO);
    Task<bool> UserExistsAsync(int userId); // UserExistsAsync should return bool
}

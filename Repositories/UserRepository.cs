using HomeLoanApplication.Data;
using HomeLoanApplication.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly HomeLoanContext _context;

    public UserRepository(HomeLoanContext context)
    {
        _context = context;
    }

    public async Task<int> AddUserAsync(User user)
    {
        // Add the new user to the database
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();  // Save changes to the database
        return user.UserId;  // Return the UserId or any other identifier for the new user
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByNameAsync(object applicationId)
    {
        throw new NotImplementedException();
    }

    // Implementation of GetByEmailAsync
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);  // Adjust according to your model and DB context
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExistsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    Task IUserRepository.GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}

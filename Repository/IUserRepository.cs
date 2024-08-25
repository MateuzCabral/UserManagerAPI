using Api.KmgShop.UserManager.Models;

namespace Api.KmgShop.UserManager.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUserAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync(int userId);
    Task<User> VerifyEmailAsync(string email);
}

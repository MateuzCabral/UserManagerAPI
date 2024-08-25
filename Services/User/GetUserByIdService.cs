using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.GetByIdUser;

public class GetUserByIdService
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user;
    }
}

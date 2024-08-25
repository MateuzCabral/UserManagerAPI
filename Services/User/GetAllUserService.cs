using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.GetAllUser;

public class GetAllUserService
{
    private readonly IUserRepository _userRepository;

    public GetAllUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        return await _userRepository.GetAllUserAsync();
    }
}

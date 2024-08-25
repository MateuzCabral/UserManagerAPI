using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.AddAddress;

public class AddAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;

    public AddAddressService(IAddressRepository addressRepository, IUserRepository userRepository)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
    }

    public async Task<Address> AddAddressAsync(int userId, AddressDTO addressDto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return null;

        var address = new Address
        {
            UserId = userId,
            City = addressDto.City,
            State = addressDto.State,
            Region = addressDto.Region,
            CEP = addressDto.CEP
        };

        await _addressRepository.AddAddressAsync(address);
        return address;
    }
}

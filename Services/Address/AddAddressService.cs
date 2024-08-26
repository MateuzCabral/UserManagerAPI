using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using System.Security.Claims;

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

    public async Task<Address> AddAddressAsync(CreateAddressDTO addressDto, ClaimsPrincipal userClaims)
    {
        int idUser = int.Parse(userClaims.FindFirstValue("id"));
        if (idUser == null) return null;

        var address = new Address
        {
            UserId = idUser,
            City = addressDto.City,
            State = addressDto.State,
            Region = addressDto.Region,
            CEP = addressDto.CEP
        };

        await _addressRepository.AddAddressAsync(address);
        return address;
    }
}

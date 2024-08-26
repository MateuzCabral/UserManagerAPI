using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using System.Security.Claims;

namespace Api.KmgShop.UserManager.Services.UpdateAddress;

public class UpdateAddressService
{
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> UpdateAddressAsync(int addressId, UpdateAddressDto addressDto, ClaimsPrincipal userClaims)
    {
        var address = await _addressRepository.GetAddressByIdAsync(addressId);

        int idUser = int.Parse(userClaims.FindFirstValue("id"));
        if (idUser != address.UserId) return null;

           address.City = addressDto.City;
           address.State = addressDto.State;
           address.Region = addressDto.Region;
           address.CEP = addressDto.CEP;

        return await _addressRepository.UpdateAddressAsync(address);
    }
}

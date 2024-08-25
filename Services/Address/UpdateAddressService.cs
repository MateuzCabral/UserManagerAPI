using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.UpdateAddress;

public class UpdateAddressService
{
    private readonly IAddressRepository _addressRepository;

    public UpdateAddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> UpdateAddressAsync(int addressId, UpdateAddressDto addressDto)
    {
        var address = await _addressRepository.GetAddressByIdAsync(addressId);
        if (address != null)
        {
            address.City = addressDto.City;
            address.State = addressDto.State;
            address.Region = addressDto.Region;
            address.CEP = addressDto.CEP;
            await _addressRepository.UpdateAddressAsync(address);
        }
        return address;
    }
}

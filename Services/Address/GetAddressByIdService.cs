using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.GetAddressById;

public class GetAddressByIdService
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIdService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        var address = await _addressRepository.GetAddressByIdAsync(addressId);
        return address;
    }
}

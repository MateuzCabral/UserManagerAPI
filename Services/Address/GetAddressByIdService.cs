using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;
using System.Security.Claims;

namespace Api.KmgShop.UserManager.Services.GetAddressById;

public class GetAddressByIdService
{
    private readonly IAddressRepository _addressRepository;

    public GetAddressByIdService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId, ClaimsPrincipal userClaims)
    {
        var address = await _addressRepository.GetAddressByIdAsync(addressId);
        if (address == null) return null;

        int idUser = int.Parse(userClaims.FindFirstValue("id"));
        if (idUser != address.UserId) return null;

        return address;
    }
}

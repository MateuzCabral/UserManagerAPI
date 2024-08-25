using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Repository;

namespace Api.KmgShop.UserManager.Services.GetAllAddress;

public class GetAllAddressService
{
    private readonly IAddressRepository _addressRepository;

    public GetAllAddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<IEnumerable<Address>> GetAllAddressesAsync()
    {
        return await _addressRepository.GetAllAddressAsync();
    }
}

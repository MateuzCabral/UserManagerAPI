using Api.KmgShop.UserManager.Models;

namespace Api.KmgShop.UserManager.Repository;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllAddressAsync();
    Task<Address> GetAddressByIdAsync(int addressId);
    Task<Address> AddAddressAsync(Address address);
    Task<Address> UpdateAddressAsync(Address address);
    Task<Address> DeleteAddressAsync(Address address);
}

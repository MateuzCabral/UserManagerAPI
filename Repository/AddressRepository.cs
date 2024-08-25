using Api.KmgShop.UserManager.Data.Context;
using Api.KmgShop.UserManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.KmgShop.UserManager.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    // Listar Todos os endereços
    public async Task<IEnumerable<Address>> GetAllAddressAsync()
    {
       return await _context.Address.ToListAsync();
    }

    // Listar Endereço por ID
    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        return await _context.Address.FirstOrDefaultAsync(a => a.AddressId == addressId);
    }

    // Adicionar um Endereço
    public async Task<Address> AddAddressAsync(Address address)
    {
        _context.Address.Add(address);
        await _context.SaveChangesAsync();
        return address;
    }


    // Atualizar Endereço
    public async Task<Address> UpdateAddressAsync(Address address)
    {
        _context.Address.Update(address);
        await _context.SaveChangesAsync();
        return address;
    }

    // Deletar Endeeço
    public async Task<Address> DeleteAddressAsync(Address address)
    {
        _context.Address.Remove(address);
        await _context.SaveChangesAsync();
        return address;
    }
}

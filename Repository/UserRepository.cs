using Api.KmgShop.UserManager.Data.Context;
using Api.KmgShop.UserManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.KmgShop.UserManager.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // Listar todos os Usuários
    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // Listar Usuário por ID
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    // Criar um Usuário
    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Atualizar Usuário
    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    // Deletar Usuario
    public async Task<User> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return user;
    }

    // Verificar Email existente
    public async Task<User> VerifyEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}

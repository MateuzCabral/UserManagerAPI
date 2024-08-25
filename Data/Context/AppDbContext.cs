using Api.KmgShop.UserManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.KmgShop.UserManager.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Address { get; set; }
}

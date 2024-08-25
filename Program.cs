using Api.KmgShop.UserManager.Data.Context;
using Api.KmgShop.UserManager.Repository;
using Api.KmgShop.UserManager.Services;
using Api.KmgShop.UserManager.Services.AddAddress;
using Api.KmgShop.UserManager.Services.CreateUser;
using Api.KmgShop.UserManager.Services.DeleteAddress;
using Api.KmgShop.UserManager.Services.DeleteUser;
using Api.KmgShop.UserManager.Services.GetAddressById;
using Api.KmgShop.UserManager.Services.GetAllAddress;
using Api.KmgShop.UserManager.Services.GetAllUser;
using Api.KmgShop.UserManager.Services.GetByIdUser;
using Api.KmgShop.UserManager.Services.UpdateAddress;
using Api.KmgShop.UserManager.Services.UpdateUser;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<GetAllUserService>();
builder.Services.AddScoped<GetUserByIdService>();
builder.Services.AddScoped<CreateUserService>();
builder.Services.AddScoped<UpdateUserService>();
builder.Services.AddScoped<DeleteUserService>();

builder.Services.AddScoped<GetAllAddressService>();
builder.Services.AddScoped<GetAddressByIdService>();
builder.Services.AddScoped<AddAddressService>();
builder.Services.AddScoped<UpdateAddressService>();
builder.Services.AddScoped<DeleteAddressService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

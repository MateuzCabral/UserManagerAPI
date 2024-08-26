using Api.KmgShop.UserManager.Data.Context;
using Api.KmgShop.UserManager.Repository;
using Api.KmgShop.UserManager.Services.AddAddress;
using Api.KmgShop.UserManager.Services.CreateUser;
using Api.KmgShop.UserManager.Services.DeleteAddress;
using Api.KmgShop.UserManager.Services.DeleteUser;
using Api.KmgShop.UserManager.Services.GetAddressById;
using Api.KmgShop.UserManager.Services.GetAllAddress;
using Api.KmgShop.UserManager.Services.GetAllUser;
using Api.KmgShop.UserManager.Services.GetByIdUser;
using Api.KmgShop.UserManager.Services.LoginUser;
using Api.KmgShop.UserManager.Services.UpdateAddress;
using Api.KmgShop.UserManager.Services.UpdateUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao contêiner.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<GetAllUserService>();
builder.Services.AddScoped<GetUserByIdService>();
builder.Services.AddScoped<CreateUserService>();
builder.Services.AddScoped<UpdateUserService>();
builder.Services.AddScoped<DeleteUserService>();
builder.Services.AddScoped<LoginUserService>();

builder.Services.AddScoped<GetAllAddressService>();
builder.Services.AddScoped<GetAddressByIdService>();
builder.Services.AddScoped<AddAddressService>();
builder.Services.AddScoped<UpdateAddressService>();
builder.Services.AddScoped<DeleteAddressService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

// Configurando o Swagger para suportar autenticação JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KmgShop UserManager API", Version = "v1" });

    // Definindo o esquema de segurança JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. \r\n\r\n " +
                      "Digite 'Bearer' [espaço] e então seu token no campo de texto abaixo.\r\n\r\n" +
                      "Exemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configurando o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KmgShop UserManager API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

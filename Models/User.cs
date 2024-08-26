using System.Text.Json.Serialization;

namespace Api.KmgShop.UserManager.Models;

public enum Role{
    Admin,
    User
}

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;
    [JsonIgnore]
    public ICollection<Address> Address { get; set; }

    
}

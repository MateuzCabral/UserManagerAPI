using System.Text.Json.Serialization;

namespace Api.KmgShop.UserManager.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    [JsonIgnore]
    public ICollection<Address> Address { get; set; }
}

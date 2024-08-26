using System.Text.Json.Serialization;

namespace Api.KmgShop.UserManager.DTOs;

public class CreateAddressDTO
{
    public string City { get; set; }
    public string State { get; set; }
    public string Region { get; set; }
    public string CEP { get; set; }
}

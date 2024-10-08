﻿using System.Text.Json.Serialization;

namespace Api.KmgShop.UserManager.Models;

public class Address
{
    public int AddressId { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Region { get; set; }
    public string CEP { get; set; }
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }

}

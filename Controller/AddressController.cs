using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Models;
using Api.KmgShop.UserManager.Services.AddAddress;
using Api.KmgShop.UserManager.Services.CreateUser;
using Api.KmgShop.UserManager.Services.DeleteAddress;
using Api.KmgShop.UserManager.Services.DeleteUser;
using Api.KmgShop.UserManager.Services.GetAddressById;
using Api.KmgShop.UserManager.Services.GetAllAddress;
using Api.KmgShop.UserManager.Services.GetAllUser;
using Api.KmgShop.UserManager.Services.UpdateAddress;
using Api.KmgShop.UserManager.Services.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace Api.KmgShop.UserManager.Controller;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly GetAllAddressService _getAllAddressService;
    private readonly GetAddressByIdService _getAddressByIdService;
    private readonly AddAddressService _addAddressService;
    private readonly UpdateAddressService _updateAddressService;
    private readonly DeleteAddressService _deleteAddressService;

    public AddressController(GetAllAddressService getAllAddressService, GetAddressByIdService getAddressByIdService, AddAddressService addAddressService, UpdateAddressService updateAddressService, DeleteAddressService deleteAddressService)
    {
        _getAllAddressService = getAllAddressService;
        _getAddressByIdService = getAddressByIdService;
        _addAddressService = addAddressService;
        _updateAddressService = updateAddressService;
        _deleteAddressService = deleteAddressService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllAddress()
    {
        var address = await _getAllAddressService.GetAllAddressesAsync();
        if (address == null || !address.Any()) return NotFound("Endereços não encontrados");
        return Ok(address);
    }

    [HttpGet("get/{addressId}")]
    public async Task<ActionResult> GetAddressById(int addressId)
    {
        var address = await _getAddressByIdService.GetAddressByIdAsync(addressId);
        if (address != null)
        {
            return Ok(address);
        }
        return NotFound("Endereço não encontrado");
    }

    [HttpPost("register")]
    public async Task<ActionResult> AddAddress(CreateAddressDTO addressDto)
    {
        var address = await _addAddressService.AddAddressAsync(addressDto);

        if (address == null) return NotFound("Usuário não encontrado");

        return Ok(address);
    }

    [HttpPut("update/{addressId}")]
    public async Task<ActionResult> UpdateAddres(int addressId, UpdateAddressDto addressDto)
    {
        var addressExist = await _getAddressByIdService.GetAddressByIdAsync(addressId);

        if (addressExist == null)
        {
            return NotFound("Endereço não encontrado");
        }

        await _updateAddressService.UpdateAddressAsync(addressId, addressDto);
        return Ok("Endereço Atualizado com Sucesso");
    }

    [HttpDelete("delete/{addressId}")]
    public async Task<IActionResult> DeleteAddress(int addressId)
    {
        var address = await _deleteAddressService.DeleteAddressAsync(addressId);
        if (address == null) return NotFound("Endereço não encontrado");
        return Ok("Endereço deletado com sucesso");
    }

}
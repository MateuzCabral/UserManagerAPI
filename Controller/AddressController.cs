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
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult> GetAllAddress()
    {
        var address = await _getAllAddressService.GetAllAddressesAsync();
        if (address == null || !address.Any()) return NotFound("Endereços não encontrados");
        return Ok(address);
    }

    // Só o proprio usuário consegue ver
    [HttpGet("get/{addressId}")]
    public async Task<ActionResult> GetAddressById(int addressId)
    {
        var address = await _getAddressByIdService.GetAddressByIdAsync(addressId, User);
        if (address == null) return NotFound("Endereço não encontrado ou permissão negada");
        
        return Ok(address);
        
        
    }

    // O id do user pega do proprio token de login
    [Authorize]
    [HttpPost("register")]
    public async Task<ActionResult> AddAddress(CreateAddressDTO addressDto)
    {
        var address = await _addAddressService.AddAddressAsync(addressDto, User);

        if (address == null) return NotFound("Usuário não encontrado ou token inválido");

        return Ok(address);
    }

    //Apenas o proprio usuario pode atualizar seu proprio endereço
    [Authorize]
    [HttpPatch("update/{addressId}")]
    public async Task<ActionResult> UpdateAddres(int addressId, UpdateAddressDto addressDto)
    {
        var address = await _updateAddressService.UpdateAddressAsync(addressId, addressDto, User);

        if (address == null)
        {
            return NotFound("Endereço não encontrado ou permissão negada");
        }

        return Ok("Endereço Atualizado com Sucesso");
    }


    //Apenas o proprio usuario pode deletar seu proprio endereço
    [Authorize]
    [HttpDelete("delete/{addressId}")]
    public async Task<IActionResult> DeleteAddress(int addressId)
    {
        var address = await _deleteAddressService.DeleteAddressAsync(addressId, User);
        if (address == null) return NotFound("Endereço não encontrado ou permissão negada");
        return Ok("Endereço deletado com sucesso");
    }

}
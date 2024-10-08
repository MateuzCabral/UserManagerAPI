﻿using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Services.CreateUser;
using Api.KmgShop.UserManager.Services.DeleteUser;
using Api.KmgShop.UserManager.Services.GetAllUser;
using Api.KmgShop.UserManager.Services.GetByIdUser;
using Api.KmgShop.UserManager.Services.LoginUser;
using Api.KmgShop.UserManager.Services.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.KmgShop.UserManager.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GetAllUserService _getAllUserService;
    private readonly GetUserByIdService _getByIdService;
    private readonly CreateUserService _createUserService;
    private readonly UpdateUserService _updateUserService;
    private readonly DeleteUserService _deleteUserService;
    private readonly LoginUserService _loginUserService;

    public UserController(GetAllUserService getAllUserService, GetUserByIdService getByIdService, CreateUserService createUserService, UpdateUserService updateUserService, DeleteUserService deleteUserService, LoginUserService loginUserService)
    {
        _getAllUserService = getAllUserService;
        _getByIdService = getByIdService;
        _createUserService = createUserService;
        _updateUserService = updateUserService;
        _deleteUserService = deleteUserService;
        _loginUserService = loginUserService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult> GetAllUser()
    {
        var user = await _getAllUserService.GetAllUserAsync();
        if (user == null || !user.Any()) return NotFound("Usuários não encontrado");
        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserById(int userId)
    {
        var user = await _getByIdService.GetUserByIdAsync(userId);
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound("Usuário não encontrado");
    }

    [HttpPost("register")]
    public async Task<ActionResult> CreateUser(UserDTO userDto)
    {
        var user = await _createUserService.CreateUserAsync(userDto);
        if (user == null) return BadRequest("Este Email já está cadastrado");
        return Ok(user);
    }

    //Apenas o proprio usuario pode atualizar seu usuario
    [Authorize]
    [HttpPatch("update")]
    public async Task<ActionResult> UpdateUser(UserDTO userDto)
    {

        var user = await _updateUserService.UpdateUserAsync(userDto, User);

        if(user == null)
        {
            return NotFound("Usuário não encontrado ou permissão negada");
        }

        return Ok("Usuário Atualizado com Sucesso");
    }

    //Apenas o proprio usuario pode deletar sua conta
    [Authorize]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _deleteUserService.DeleteUserAsync(userId, User);
        if (user == null) return NotFound("Usuário não encontrado ou permissão negada");
        return Ok("Usuário deletado com sucesso");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginDTO loginDto)
    {
        var user = await _loginUserService.LoginAsync(loginDto);
        if (user == null) return BadRequest("Email ou Senha Incorreto");
        return Ok(user);
    }
}

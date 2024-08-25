using Api.KmgShop.UserManager.DTOs;
using Api.KmgShop.UserManager.Services;
using Api.KmgShop.UserManager.Services.CreateUser;
using Api.KmgShop.UserManager.Services.DeleteUser;
using Api.KmgShop.UserManager.Services.GetAllUser;
using Api.KmgShop.UserManager.Services.GetByIdUser;
using Api.KmgShop.UserManager.Services.UpdateUser;
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

    public UserController(GetAllUserService getAllUserService, GetUserByIdService getByIdService, CreateUserService createUserService, UpdateUserService updateUserService, DeleteUserService deleteUserService)
    {
        _getAllUserService = getAllUserService;
        _getByIdService = getByIdService;
        _createUserService = createUserService;
        _updateUserService = updateUserService;
        _deleteUserService = deleteUserService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUser()
    {
        var user = await _getAllUserService.GetAllUserAsync();
        if (user == null || !user.Any()) return NotFound("Usuários não encontrado");
        return Ok(user);
    }

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

    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser(int userId, UserDTO userDto)
    {
        var userExist = await _getByIdService.GetUserByIdAsync(userId);

        if(userExist == null)
        {
            return NotFound("Usuário não encontrado");
        }

        await _updateUserService.UpdateUserAsync(userId, userDto);
        return Ok("Usuário Atualizado com Sucesso");
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var user = await _deleteUserService.DeleteUserAsync(userId);
        if (user == null) return NotFound("Usuário não encontrado");
        return Ok("Usuário deletado com sucesso");
    }
}

using Microsoft.AspNetCore.Mvc;
using NorthwindWebApi.Services;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    UserService userService;


    /// <summary>
    /// Login with Username and Password and get Jwt token
    /// </summary>
    [HttpPost("Login")]
    public async Task<ActionResult<UserProfileDto>> LoginAsync([FromBody] LoginDto loginDto)
    {
        return Ok(await userService.LoginAsync(loginDto));
    }

    /// <summary>
    /// Create new Northwind User credentials
    /// </summary>
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    {
         await userService.CreateUserAsync(createUserDto);
         return Ok();
    }

    /// <summary>
    /// Create new Northwind Role
    /// </summary>
    [HttpGet("CreateRole")]
    public async Task<IActionResult> CreateRoleAsync(String roleName)
    {
        await userService.CreateRoleAsync(roleName);
        return Ok();
    }

    /// <summary>
    /// Assign Role to a User in Northwind
    /// </summary>
    [HttpGet("AssingRoleToUser")]
    public async Task<IActionResult> AssignRoleToUserAsync(String userName , String roleName)
    {
         await userService.AssignRoleToUserAsync(roleName , userName);
         return Ok();
    }
    
    public UserController(UserService userService)
    {
        this.userService = userService; 
    }
}
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApi.Services;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;

[Route("[controller]")]
public class UserController : ControllerBase
{
    UserService userService;
    
    [HttpPost("Login")]
    public async Task<ActionResult<UserProfileDto>> LoginAsync([FromBody] LoginDto loginDto)
    {
        return Ok(await userService.LoginAsync(loginDto));
    }
    
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    {
         await userService.CreateUserAsync(createUserDto);
         return Ok();
    }
    
    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRoleAsync(String roleName)
    {
        await userService.CreateRoleAsync(roleName);
        return Ok();
    }
    
    [HttpPost("AssingRoleToUser")]
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
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApi.Services;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;

[Route("[controller]")]
public class UserController : ControllerBase
{
    UserService userService;
    
    [HttpPost("Login")]
    public async Task<ActionResult<UserProfileDto>> Login([FromBody] LoginDto loginDto)
    {
        return null; 
    }

    public UserController(UserService userService)
    {
        this.userService = userService; 
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.Exceptions;
using NorthwindWebApi.Security;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Services;

public class UserService
{
    private NorthwindDataContext _dataContext;
    private IJwtService _jwtService;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private SignInManager<ApplicationUser> _signInManager;
    private AppConfiguration _appConfig;

    public UserService(IJwtService jwtService, NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig,
        UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager , SignInManager<ApplicationUser> signInManager
    )
    {
        _dataContext = dataContext;
        _jwtService = jwtService;
        _userManager = userManager;
        _roleManager = roleManager;
        _appConfig =  appConfig.Value;
        _signInManager = signInManager;
    }

    public async Task<UserProfileDto> LoginAsync(LoginDto loginDto)
    {
        var user  = await _userManager.FindByNameAsync(loginDto.UserName);
        var role = await _userManager.GetRolesAsync(user);
        
        //var result = await _userManager.CheckPasswordAsync(user1 , loginDto.Password);
        return await _jwtService.CreateJwtToken(user, role[0]);
    }
    
    public async Task AssignRoleToUserAsync(String roleName, String userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
            throw new NorthwindWebApiException(ErrorMessages.UserNotFound ,  ErrorType.BusinessError.ToString());   
        
        
        var role = await _roleManager.FindByNameAsync(roleName);
        
        if (user == null)
            throw new NorthwindWebApiException(ErrorMessages.RoleNotFound ,  ErrorType.BusinessError.ToString()); 
        
        await _userManager.RemoveFromRoleAsync(user , roleName);
        var result = await _userManager.AddToRoleAsync(user , roleName);
        
        if (!result.Succeeded)
            throw new NorthwindWebApiException(result.Errors.First().Description , ErrorType.BusinessError.ToString());
    }

    public async Task CreateUserAsync(CreateUserDto createUserDto)
    {
        ApplicationUser user = new ApplicationUser();
        
        user.FullName = createUserDto.FullName;
        user.UserName = createUserDto.UserName;
        user.Email = createUserDto.Email;
        user.DateOfBirth = createUserDto.BirthDate;
        
        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        
        if (!result.Succeeded)
            throw new NorthwindWebApiException(result.Errors.First().Description , ErrorType.BusinessError.ToString());
    }
    
    public async Task CreateRoleAsync(String roleName)
    {
        ApplicationRole role = new ApplicationRole();
        role.Name = roleName;
        
        var result = await _roleManager.CreateAsync(role);
        
        if (!result.Succeeded)
            throw new NorthwindWebApiException(result.Errors.First().Description ,  ErrorType.BusinessError.ToString());
        
        
    }
}
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

    private Role ValidateAndGetRoleFromString(String roleName)
    {

        try
        {
            return Enum.Parse<Role>(roleName, false);
        }

        catch (Exception)
        {
            throw new NorthwindWebApiException(ErrorMessages.RoleNotValid, ErrorType.BusinessError.ToString());
        }

    }

    public UserService(IJwtService jwtService, NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig,
        UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager
    )
    {
        _dataContext = dataContext;
        _jwtService = jwtService;
        _userManager = userManager;
        _roleManager = roleManager;
        _appConfig = appConfig.Value;
        _signInManager = signInManager;
    }

    public async Task<UserProfileDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user == null)
            throw new NorthwindWebApiException(ErrorMessages.AuthenticationError, ErrorType.AuthenticationError.ToString());

        var passwordValidation = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!passwordValidation.Succeeded)
            throw new NorthwindWebApiException(ErrorMessages.AuthenticationError, ErrorType.AuthenticationError.ToString());

        var role = await _userManager.GetRolesAsync(user);

        if (role == null || role.Count == 0)
            throw new NorthwindWebApiException(ErrorMessages.UserRoleNotFound, ErrorType.BusinessError.ToString());


        return await _jwtService.CreateJwtToken(user, role[0]);
    }

    public async Task AssignRoleToUserAsync(String roleName, String userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
            throw new NorthwindWebApiException(ErrorMessages.UserNotFound, ErrorType.BusinessError.ToString());

        ValidateAndGetRoleFromString(roleName);

        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null)
            throw new NorthwindWebApiException(ErrorMessages.UserRoleNotFound, ErrorType.BusinessError.ToString());

        await _userManager.RemoveFromRoleAsync(user, roleName);
        var result = await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
            throw new NorthwindWebApiException(result.Errors.First().Description, ErrorType.BusinessError.ToString());
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
            throw new NorthwindWebApiException(result.Errors.First().Description, ErrorType.BusinessError.ToString());
    }

    public async Task CreateRoleAsync(String roleName)
    {
        ValidateAndGetRoleFromString(roleName);

        ApplicationRole role = new ApplicationRole();
        role.Name = roleName;

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            throw new NorthwindWebApiException(result.Errors.First().Description, ErrorType.BusinessError.ToString());


    }
}
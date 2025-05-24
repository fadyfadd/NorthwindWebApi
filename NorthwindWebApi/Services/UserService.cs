using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.Security;

namespace NorthwindWebApi.Services;

public class UserService
{
    private NorthwindDataContext _dataContext;
    private IJwtService _jwtService;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;
    private AppConfiguration _appConfig;

    public UserService(IJwtService jwtService, NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig,
        UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager
    )
    {
        _dataContext = dataContext;
        _jwtService = jwtService;
        _userManager = userManager;
        _roleManager = roleManager;
        _appConfig =  appConfig.Value;
    }
}
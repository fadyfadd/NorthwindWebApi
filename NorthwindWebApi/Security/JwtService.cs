using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindWebApi.Configuration;
using NorthwindWebApi.Exceptions;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public class JwtService : IJwtService
{
    private AppConfiguration _appConfig;

    public async Task<UserProfileDto> CreateJwtToken(ApplicationUser user, String userRole)
    {
        DateTime expiration = DateTime.Now.AddMinutes(_appConfig.JwtConfiguration.ExpirationInMinutes);

        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, userRole),
            
        };

        SymmetricSecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JwtConfiguration.Key));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = signingCredentials,
            
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(securityTokenDescriptor);

        var userProfileDto = new UserProfileDto()
        {
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            UserRole = userRole,
            Token = tokenHandler.WriteToken(token),
            TokenExpirationInMin = _appConfig.JwtConfiguration.ExpirationInMinutes,
            RefreshTokenExpirationInMin = _appConfig.JwtConfiguration.RefreshTokenExpirationInMinutes,
            RefreshToken = GenerateRefreshToken(),
        };
        
        return userProfileDto;
    }

    public JwtService(IOptions<AppConfiguration> appConfig)
    {
        _appConfig = appConfig.Value;
    }

    private string GenerateRefreshToken()
    {
        byte[] bytes = new byte[64];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public async Task<ClaimsPrincipal> GetPrincipalFromJwtToken(string? token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JwtConfiguration.Key)),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal =
                jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters,
                    out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new NorthwindWebApiException(ErrorMessages.AuthenticationError,  ErrorType.AuthenticationError.ToString());
            }
            
            return principal;
            
        }
        catch (Exception ex)
        {
            throw new NorthwindWebApiException(ErrorMessages.AuthenticationError,  ErrorType.AuthenticationError.ToString(), null , ex);
        }
    }
}
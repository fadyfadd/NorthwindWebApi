using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NorthwindWebApi.Configuration;
using NorthwindWebApi.Services;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public class JwtService : IJwtService
{
    private AppConfiguration _appConfig;

    public UserProfileDto CreateJwtToken(ApplicationUser user, Role userRole)
    {
        DateTime expiration = DateTime.Now.AddMinutes(_appConfig.JwtConfiguration.ExpirationInMinutes);

        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };


        SymmetricSecurityKey securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JwtConfiguration.Key));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenGenerator = new JwtSecurityToken(
            _appConfig.JwtConfiguration.Issuer,
            _appConfig.JwtConfiguration.Audience,
            claims,
            expires: expiration,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        var userProfileDto = new UserProfileDto()
        {
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            UserRole = userRole,
            Token = tokenHandler.WriteToken(tokenGenerator),
            RefreshTokenExpirationDateTime = expiration,
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

    public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = _appConfig.JwtConfiguration.Audience,
            ValidateIssuer = true,
            ValidIssuer = _appConfig.JwtConfiguration.Issuer,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.JwtConfiguration.Key)),

            ValidateLifetime = false //should be false
        };

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal principal =
            jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}
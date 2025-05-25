namespace NorthwindWebApi.Configuration;

public class JwtConfiguration
{
    public String Issuer { get; set; }
    public String Audience { get; set; }    
    public String Key { get; set; }
    public Int32 ExpirationInMinutes { get; set; }
    public Int32 RefreshTokenExpirationInMinutes {set ; get; }
}
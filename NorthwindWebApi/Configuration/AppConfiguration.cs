namespace NorthwindWebApi.Configuration;

public class AppConfiguration
{
    public CorsConfiguration? CorsConfiguration { get; set; }
    public JwtConfiguration? JwtConfiguration { get; set; }
    public SwaggerConfiguration? SwaggerConfiguration { get; set; }
    public DatabaseConfiguration? DatabaseConfiguration { get; set; }
}
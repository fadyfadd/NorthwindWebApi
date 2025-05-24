namespace NorthwindWebApi.Configuration;

public class CorsConfiguration
{
    public String[] WithOrigins { get; set; }
    public String[] WithMethods { get; set; }
    public String[] WithHeaders { get; set; }
}
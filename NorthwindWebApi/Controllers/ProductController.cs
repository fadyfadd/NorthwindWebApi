using Microsoft.AspNetCore.Mvc;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;

 
[ApiController]
[Route("[controller]")]

public class ProductController : ControllerBase
{
 
    NorthwindDataContext _context;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger , NorthwindDataContext dataContext)
    {
        _context = dataContext;
        _logger = logger;
    }

    /// <summary>
    /// Get all Products
    /// </summary>
    [HttpGet("All")]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var products = _context.Products.ToList();
        return Ok(new List<ProductDto>());
    }
 
}
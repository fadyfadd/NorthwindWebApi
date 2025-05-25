using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;


[Route("[controller]")]
public class ProductController : ControllerBase
{
 
    NorthwindDataContext _context;
    private readonly ILogger<ProductController> _logger;
    IMapper _mapper;
    
    public ProductController(ILogger<ProductController> logger , NorthwindDataContext dataContext , IMapper mapper)
    {
        _context = dataContext;
        _logger = logger;
        _mapper = mapper;
    }

   
    /// <summary>
    /// Get all Products
    /// </summary>
    [Authorize(Roles = "StandardUser")]   
    [HttpGet("All")]
    public async Task<ActionResult<List<ProductDto>>> GetProductsAsync()
    {
        var products = _context.Products.ToList();
        var dtos = _mapper.Map<List<ProductDto>>(products);
        return Ok(dtos);
    }
 
}
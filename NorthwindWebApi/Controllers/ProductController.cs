using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApi.Services;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace NorthWindWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
 
 
    private readonly ILogger<ProductController> _logger;
    private ProductService _productService;
    IMapper _mapper;
    
    public ProductController(ILogger<ProductController> logger , NorthwindDataContext dataContext , IMapper mapper , ProductService productService)
    { 
        _logger = logger;
        _mapper = mapper;
        _productService = productService;
    }
    /// <summary>
    /// Get Product by Id
    /// </summary>
    //[Authorize(Roles = "StandardUser")]   
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductByIdAsync(Int32 id)
    {
        var dto = _productService.GetProductById(id);
        return Ok(dto);
    }

   
    /// <summary>
    /// Get all Products
    /// </summary>
    //[Authorize(Roles = "StandardUser")]   
    [HttpGet("All")]
    public async Task<ActionResult<List<ProductDto>>> GetProductsAsync()
    {      
        var dtos = _productService.GetAllProducts();     
        return Ok(dtos);
    }
 
}
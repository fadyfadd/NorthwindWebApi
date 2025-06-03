using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebApi.Services;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using NorthwindWebApi.Filters;

namespace NorthWindWebApi.Controllers;

[ApiController]
//[ServiceFilter(typeof(LoggingFilter))]
[Route("[controller]")]
public class ProductController : ControllerBase
{
 
 
    private readonly ILogger<ProductController> _logger;
    private ProductService _productService;

    
    public ProductController(ILogger<ProductController> logger , ProductService productService)
    { 
        _logger = logger;
 
        _productService = productService;
    }
    /// <summary>
    /// Get Product by Id
    /// </summary>
    //[Authorize(Roles = "StandardUser")]   
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductByIdAsync(Int32 id)
    {
        var dto = await _productService.GetProductByIdAsync(id);
        return Ok(dto);
    }

    /// <summary>
    /// Save or Update Product
    /// </summary>
    [HttpPost("SaveOrUpdate")]
    public async Task<ActionResult<ProductDto>> SaveProductAsync([FromBody] ProductDto productDto)
    {
        return  Ok(_productService.SaveOrUpdateProduct(productDto)); 
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
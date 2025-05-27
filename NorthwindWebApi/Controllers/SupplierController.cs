using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using NorthwindWebApi.Services;

namespace NorthWindWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SupplierController : ControllerBase
{
    
    private readonly ILogger<SupplierController> _logger;
    private SupplierService _supplierService;
 
    
    public SupplierController(ILogger<SupplierController> logger , NorthwindDataContext dataContex  , SupplierService supplierService)
    { 
        _logger = logger;
        _supplierService = supplierService;
    }
    
    /// <summary>
    /// Get Supplier by Id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetSupplierByIdAsync(Int32 id , Boolean includeProducts = false)
    {
        var dto = _supplierService.GetSupplierById(id , includeProducts);
        return Ok(dto);
    }

   
    /// <summary>
    /// Get all Suppliers
    /// </summary>
    [HttpGet("All")]
    public async Task<ActionResult<List<SupplierDto>>> GetSuppliersAsync(Boolean includeProducts = false)
    {      
        var dtos = _supplierService.GetAllSuppliers(includeProducts);     
        return Ok(dtos);
    }
    
    
    
}
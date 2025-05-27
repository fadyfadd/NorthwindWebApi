using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using NorthwindWebApi.Services;

namespace NorthWindWebApi.Controllers;

[Route("[controller]")]
public class SupplierController : ControllerBase
{
    
    private readonly ILogger<SupplierController> _logger;
    private SupplierService _supplierService;
    IMapper _mapper;
    
    public SupplierController(ILogger<SupplierController> logger , NorthwindDataContext dataContext , IMapper mapper , SupplierService supplierService)
    { 
        _logger = logger;
        _mapper = mapper;
        _supplierService = supplierService;
    }
    
    /// <summary>
    /// Get Supplier by Id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetSupplierByIdAsync(Int32 id , Boolean includeDetails = false)
    {
        var dto = _supplierService.GetSupplierById(id , includeDetails);
        return Ok(dto);
    }

   
    /// <summary>
    /// Get all Suppliers
    /// </summary>
    [HttpGet("All")]
    public async Task<ActionResult<List<SupplierDto>>> GetSuppliersAsync()
    {      
        var dtos = _supplierService.GetAllSuppliers();     
        return Ok(dtos);
    }
    
    
    
}
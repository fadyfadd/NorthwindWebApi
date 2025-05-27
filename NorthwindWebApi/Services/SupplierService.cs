using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthwindWebApi.DataAccessLayer;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Services;

public class SupplierService
{
    private NorthwindDataContext _dataContext;
    private AppConfiguration _appConfig;
    private IMapper _mapper;

    List<ProductDto> GetProductsFromSupplier(Supplier supplier)
    {
        List<ProductDto> products = new List<ProductDto>();
        
        foreach (var product in supplier.Products)
        {
            products.Add(_mapper.Map<ProductDto>(product));
        }
        return products;
    }
    
    public SupplierService(NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig, IMapper mapper)
    {
        _dataContext = dataContext;
        _appConfig = appConfig.Value;
        _mapper = mapper;
    }

    public SupplierDto GetSupplierById(Int32 id , Boolean includeProducts)
    {
        if (includeProducts)
        {
            var supplier = _dataContext.Suppliers.Include(s => s.Products)
                .Where(s => s.SupplierId == id)
                .FirstOrDefault();
            
            var output = _mapper.Map<SupplierDto>(supplier);
            output.Products = GetProductsFromSupplier(supplier);
            
            return output;
        }
        
        return _mapper.Map<SupplierDto>(_dataContext.Suppliers
            .FirstOrDefault(s => s.SupplierId == id));
    }

    public List<SupplierDto> GetAllSuppliers()
    {
        var products = _dataContext.Suppliers.ToList();
        return _mapper.Map<List<SupplierDto>>(products);
    }
}
using AutoMapper;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;

namespace NorthwindWebApi.Services;

public class SupplierService
{
    private NorthwindDataContext _dataContext;
    private AppConfiguration _appConfig;
    private IMapper _mapper;

    public SupplierService(NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig, IMapper mapper)
    {
        _dataContext = dataContext;
        _appConfig = appConfig.Value;
        _mapper = mapper;
    }

    public SupplierDto GetSupplierById(Int32 id)
    {
        return _mapper.Map<SupplierDto>(_dataContext.Suppliers
            .FirstOrDefault(s => s.SupplierId == id));
    }

    public List<SupplierDto> GetAllSuppliers()
    {
        var products = _dataContext.Suppliers.ToList();
        return _mapper.Map<List<SupplierDto>>(products);
    }
}
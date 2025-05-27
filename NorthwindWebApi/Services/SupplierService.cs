using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthwindWebApi.DataAccessLayer;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using NorthwindWebApi.Exceptions;
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

    public SupplierDto GetSupplierById(Int32 id, Boolean includeProducts)
    {
        if (includeProducts)
        { 
            var supplier = _dataContext.Suppliers
                .Include(s => s.Products)
                .FirstOrDefault(s => s.SupplierId == id);

            if (supplier == null)
                throw new NorthwindWebApiException(ErrorMessages.SupplierNotFound, ErrorType.BusinessError.ToString());

            var output = _mapper.Map<SupplierDto>(supplier);
            output.Products = GetProductsFromSupplier(supplier);

            return output;
        }
        else
        {
            var supplier =  _mapper.Map<SupplierDto>(_dataContext.Suppliers
                .FirstOrDefault(s => s.SupplierId == id));
            
            if (supplier == null)
                throw new NorthwindWebApiException(ErrorMessages.SupplierNotFound, ErrorType.BusinessError.ToString());
            
            return supplier;
            
        }


    }

    public List<SupplierDto> GetAllSuppliers(Boolean includeProducts)
    {
        if (includeProducts)
        {
            var suppliers = _dataContext.Suppliers.Include(s=>s.Products).ToList();
            List<SupplierDto> supplierDtos = new List<SupplierDto>();

            foreach (var supplier in suppliers)
            {
                var currentSupplier = _mapper.Map<SupplierDto>(supplier);
                currentSupplier.Products = GetProductsFromSupplier(supplier);
                supplierDtos.Add(currentSupplier);
            }

            return supplierDtos;
        }
        else
        {
            var suppliers = _dataContext.Suppliers.ToList();
            return _mapper.Map<List<SupplierDto>>(suppliers);
        }
    }
}
using AutoMapper;
using NorthwindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
using NorthwindWebApi.Security;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace WebApiNorthwind.Mappers;

public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ForMember(s => s.Products, opt => opt.Ignore());
        CreateMap<SupplierDto, Supplier>();
    }
}
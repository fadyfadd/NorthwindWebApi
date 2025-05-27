using AutoMapper;
using NorthwindWebApi.DataAccessLayer;
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.DataTransferObject;
 
namespace NorthwindWebApi.Mappers;

public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ForMember(s => s.Products, opt => opt.Ignore());
        CreateMap<SupplierDto, Supplier>();
    }
}
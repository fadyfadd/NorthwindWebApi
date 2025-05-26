using AutoMapper;
using NorthwindWebApi.Security;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace WebApiNorthwind.Mappers;

public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<Product, ProductDto>().ReverseMap();       
    }
}
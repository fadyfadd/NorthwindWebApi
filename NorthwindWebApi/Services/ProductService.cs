using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
using NorthwindWebApi.Security;
using NorthWindWebApi.DataAccessLayer;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Services
{
    public class ProductService
    {
        private NorthwindDataContext _dataContext;
        private AppConfiguration _appConfig;
        private IMapper _mapper;

        public ProductDto GetProductById(Int32 id)
        {
            return _mapper.Map<ProductDto>(_dataContext.Products.Where(p => p.ProductId == id)
                .Include((p) => p.Supplier).FirstOrDefault());
        }

        public List<ProductDto> GetAllProducts()
        {
            var products = _dataContext.Products.Include(p => p.Supplier).ToList();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public ProductService(NorthwindDataContext dataContext, IOptions<AppConfiguration> appConfig, IMapper mapper)
        {
            _dataContext = dataContext;
            _appConfig = appConfig.Value;
            _mapper = mapper;
        }
    }
}
using System.Reflection.Metadata;
using AutoMapper;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NorthwindWebApi.Configuration;
 
using NorthWindWebApi.DataAccessLayer;
using NorthwindWebApi.Exceptions;
using NorthwindWebApi.DataTransferObject;

namespace NorthwindWebApi.Services
{
   
    public class ProductService
    {
        private NorthwindDataContext _dataContext;
        private AppConfiguration _appConfig;
        private IMapper _mapper;


        public async Task<ProductDto> SaveOrUpdateProductAsync(ProductDto productDto)
        {
            if (productDto.ProductId <= 0)
            {
                var product = _mapper.Map<Product>(productDto);
                product.ProductId = null; 
                _dataContext.Products.Add(product);
                await _dataContext.SaveChangesAsync();
                return _mapper.Map<ProductDto>(product);
            }
            else
            {
                var product = _dataContext.Products.FirstOrDefault(p => p.ProductId == productDto.ProductId);
                if (product == null)
                {
                    throw new NorthwindWebApiException(ErrorMessages.ProductNotFound , ErrorType.BusinessError.ToString());
                }
                else
                {
                    product.ProductName = productDto.ProductName;
                    product.UnitPrice = productDto.UnitPrice;
                    product.UnitsInStock = productDto.UnitsInStock;
                    product.UnitsOnOrder = productDto.UnitsOnOrder;
                    product.Discontinued = productDto.Discontinued;
                    product.SupplierId = productDto.SupplierId;
                    await _dataContext.SaveChangesAsync();
                    return _mapper.Map<ProductDto>(product);
                }
            }
        }
        
        public async Task<ProductDto> GetProductByIdAsync(Int32 id)
        {
            var product =  _mapper.Map<ProductDto>(await _dataContext.Products.Where(p => p.ProductId == id)
                .Include((p) => p.Supplier).FirstOrDefaultAsync());

            if (product == null)
                throw new NorthwindWebApiException(ErrorMessages.ProductNotFound, ErrorType.BusinessError.ToString());
            
            return product; 
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products =   _dataContext.Products.Include(p => p.Supplier).OrderBy(p=>p.ProductId).ToList();
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
using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            if(product.ProductId > 0)
            {
                _context.Update(product);
            }
            else
            {
                _context.Add(product);
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                    return false;
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;   
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public  async Task<ProductDto> GetProductsById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId.Equals(productId));
            return _mapper.Map<ProductDto>(product);
        }
    }
}

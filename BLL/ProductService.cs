using N_Tier.DAL;
using N_Tier.DTOs;
using N_Tier.Entities;

namespace N_Tier.BLL
{
    public class ProductService
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(int id, ProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.Name = dto.Name;
                product.Price = dto.Price;
                product.CategoryId = dto.CategoryId;
                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}

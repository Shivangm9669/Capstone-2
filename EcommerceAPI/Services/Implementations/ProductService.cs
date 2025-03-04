using EcommerceAPI.Data;
using EcommerceAPI.Models;
using EcommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly EcommerceDbContext _context;

        public ProductService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
    await _context.Products
        .Include(p => p.Category)
        .Include(p => p.ProductImages)
        .ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int productId) =>
            await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

        public async Task<bool> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

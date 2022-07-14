using Microsoft.EntityFrameworkCore;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.Domain.Models;

namespace Online_Shop.Database
{
    public class ProductManager : IProductManager
    {
        private AppDbContext _dbContext;

        public ProductManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProduct(Product product)
        {
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductById(int id)
        {
            _dbContext.Products.Remove(_dbContext.Products.FirstOrDefault(product => product.Id == id));

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public IEnumerable<T> GetProducsWithStock<T>(Func<Product, T> selector)
            => _dbContext.Products.Include(db => db.Stock).Select(selector).ToList();

        public Product GetProductById(int id)
            => _dbContext.Products.FirstOrDefault(prod => prod.Id == id);

        public T GetProductByName<T>(string name, Func<Product, T> selector)
            => _dbContext.Products.Include(product => product.Stock).
                Where(product => product.Name == name).
                Select(selector).
                FirstOrDefault();

        public IEnumerable<T> GetProducts<T>(Func<Product, T> selector)
            => _dbContext.Products.Select(selector).ToList();

        public async Task UpdateProduct(Product product, Action<Product> updateValues)
        {
            updateValues(product);
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

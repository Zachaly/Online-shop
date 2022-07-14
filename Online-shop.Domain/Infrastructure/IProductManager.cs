using Online_Shop.Domain.Models;

namespace Online_Shop.Domain.Infrastructure
{
    public interface IProductManager
    {
        T GetProductByName<T>(string productName, Func<Product, T> selector);
        Product GetProductById(int id);
        IEnumerable<T> GetProducsWithStock<T>(Func<Product, T> selector);
        Task AddProduct(Product product);
        Task<bool> DeleteProductById(int id);
        IEnumerable<T> GetProducts<T>(Func<Product, T> selector);
        Task UpdateProduct(Product product, Action<Product> updateValues);
    }
}

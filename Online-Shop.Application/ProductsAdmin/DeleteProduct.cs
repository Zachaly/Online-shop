using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.ProductsAdmin
{
    /// <summary>
    /// Removes product from Database
    /// </summary>
    public class DeleteProduct
    {
        private readonly IProductManager _productManager;

        public DeleteProduct(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public async Task<bool> ExecuteAsync(int productId)
            => await _productManager.DeleteProductById(productId);
    }
}


namespace Online_Shop.Database.Tests
{
    public class ProductManagerTests : DbTest
    {
        private ProductManager _productManager;

        public ProductManagerTests() : base()
        {
            _productManager = new ProductManager(_dbContext);
        }

        private Product CreateValidProduct() => new Product
        {
            Description = "Valid Description",
            Name = "Valid name",
            Value = 1234,
        };

        [Fact]
        public async Task Add_Valid_Product()
        {
            var product = CreateValidProduct();

            var result = _productManager.AddProduct(product).IsCompletedSuccessfully;

            Assert.True(result);
            Assert.Contains(_dbContext.Products, product => product.Id == product.Id);
        }

        [Fact]
        public async Task Try_Add_Invalid_Product()
        {
            var product = CreateValidProduct();

            product.Name = null;

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _productManager.AddProduct(product));
        }

        [Fact]
        public async Task Delete_Product_By_Id()
        {
            var product = _dbContext.Products.FirstOrDefault(product => product.Id == 1);

            var result = await _productManager.DeleteProductById(1);

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Products, product => product.Id == 1);
        }

        [Fact]
        public void Get_Product_By_Id()
        {
            Assert.NotNull(_productManager.GetProductById(1));
        }

        [Fact]
        public void Try_Get_Product_By_Non_Existing_Id()
        {
            Assert.Null(_productManager.GetProductById(5));
        }

        [Fact]
        public void Get_Product_By_Name()
        {
            Assert.NotNull(_productManager.GetProductByName("Product 1", product => product));
        }

        [Fact]
        public void Try_Get_Product_By_Non_Existing_Name()
        {
            Assert.Null(_productManager.GetProductByName("Product 4", product => product));
        }

        [Fact]
        public void Get_Products()
        {
            var products = _productManager.GetProducts(product => product);
            Assert.Equal(3, products.Count());
        }

        [Fact]
        public void Update_Product()
        {
            var updateProduct = CreateValidProduct();
            var product = _dbContext.Products.FirstOrDefault(product => product.Id == 1);


            var result = _productManager.UpdateProduct(product, newProduct =>
            {
                newProduct.Name = updateProduct.Name;
                newProduct.Description = updateProduct.Description;
                newProduct.Value = updateProduct.Value;
            }).IsCompletedSuccessfully;

            Assert.True(result);
            Assert.Equal(updateProduct.Name, product.Name);
            Assert.Equal(updateProduct.Description, product.Description);
            Assert.Equal(updateProduct.Value, product.Value);
        }

        [Fact]
        public void Get_Products_With_Stock()
        {
            var products = _productManager.GetProducsWithStock(product => product);

            Assert.True(products.All(product => product.Stock is null == false));
        }
    }
}

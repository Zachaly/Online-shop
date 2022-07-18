
namespace Online_Shop.Database.Tests
{
    public class StockManagerTests : DbTest
    {
        private StockManager _stockManager;

        public StockManagerTests() : base()
        {
            _stockManager = new StockManager(_dbContext);
        }

        private Stock CreateValidStock() => new Stock
        {
            Description = "product 1 - stock 4",
            ProductId = 1,
            Quantity = 111
        };

        [Fact]
        public void Add_Valid_Stock()
        {
            var newStock = CreateValidStock();

            var result = _stockManager.AddStock(newStock).IsCompletedSuccessfully;

            Assert.True(result);
            Assert.Contains(_dbContext.Stock, stock => stock.Id == newStock.Id);
        }

        [Fact]
        public async Task Try_Add_InValid_Stock()
        {
            var newStock = CreateValidStock();

            newStock.Description = null;

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _stockManager.AddStock(newStock));
        }

        [Fact]
        public void Delete_Stock_By_Id()
        {
            var result = _stockManager.DeleteStockById(1).IsCompletedSuccessfully;

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Stock, stock => stock.Id == 1);
        }

        [Fact]
        public void Enought_Stock()
        {
            Assert.True(_stockManager.EnoughtStock(3, 9));
        }

        [Fact]
        public void Not_Enought_Stock()
        {
            Assert.False(_stockManager.EnoughtStock(1, 99));
        }

        [Fact]
        public void Get_Stock_With_Product()
        {
            Assert.NotNull(_stockManager.GetStockWithProduct(1));
        }

        [Fact]
        public void Try_Get_Stock_With_Product_Using_Non_Existing_Id()
        {
            Assert.Null(_stockManager.GetStockWithProduct(10));
        }

        [Fact]
        public void Put_Stock_On_Hold()
        {
            var result = _stockManager.PutStockOnHold(1, "notexistingsession", 5).IsCompletedSuccessfully;

            Assert.True(result);
            Assert.True(_dbContext.Stock.FirstOrDefault(stock => stock.Id == 1).Quantity == 5);
            Assert.Contains(_dbContext.StocksOnHold, stock => stock.StockId == 1 && stock.Quantity == 5);
        }

        [Fact]
        public async Task Refill_Stocks()
        {
            await _stockManager.PutStockOnHold(1, "nonexistingsession", 5);
            await _stockManager.PutStockOnHold(2, "nonexistingsession", 10);
            await _stockManager.PutStockOnHold(3, "nonexistingsession", 15);

            await _dbContext.StocksOnHold.ForEachAsync(stock => stock.ExpireDate = DateTime.Now - TimeSpan.FromDays(7));

            await _dbContext.SaveChangesAsync();

            await _stockManager.RefillStocks();

            //Assert.Empty(_dbContext.StocksOnHold);
            Assert.Contains(_dbContext.Stock, stock => stock.Id == 1 && stock.Quantity == 10);
            Assert.Contains(_dbContext.Stock, stock => stock.Id == 2 && stock.Quantity == 20);
            Assert.Contains(_dbContext.Stock, stock => stock.Id == 3 && stock.Quantity == 30);
        }

        [Fact]
        public async Task Remove_Stock_From_Hold()
        {
            await _stockManager.PutStockOnHold(1, "nonexistingsession", 5);
            await _stockManager.PutStockOnHold(2, "nonexistingsession", 10);

            await _stockManager.RemoveStockFromHold(1, "nonexistingsession", 5);
            await _stockManager.RemoveStockFromHold(2, "nonexistingsession", 5);

            Assert.DoesNotContain(_dbContext.StocksOnHold, stock => stock.StockId == 1);
            Assert.Contains(_dbContext.StocksOnHold, stock => stock.StockId == 2 && stock.Quantity == 5);
        }

        [Fact]
        public async Task Remove_Stock_From_Hold_By_Session_Id()
        {
            await _stockManager.PutStockOnHold(1, "nonexistingsession", 5);
            await _stockManager.PutStockOnHold(2, "nonexistingsession", 10);
            await _stockManager.PutStockOnHold(3, "nonexistingsession", 15);

            await _stockManager.RemoveStockFromHold("nonexistingsession");

            Assert.Empty(_dbContext.StocksOnHold);
        }

        [Fact]
        public async Task Update_Stocks()
        {
            var stock1 = _dbContext.Stock.FirstOrDefault(stock => stock.Id == 1);
            var stock2 = _dbContext.Stock.FirstOrDefault(stock => stock.Id == 2);

            stock1.Description = "new description 1";
            stock2.Description = "new description 2";

            await _stockManager.UpdateStocks(new List<Stock> { stock1, stock2 });

            Assert.Contains(_dbContext.Stock, stock => stock.Id == 1 && stock.Description == "new description 1");
            Assert.Contains(_dbContext.Stock, stock => stock.Id == 2 && stock.Description == "new description 2");
        }
    }
}

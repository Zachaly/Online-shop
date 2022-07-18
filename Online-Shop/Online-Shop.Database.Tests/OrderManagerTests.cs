using Online_Shop.Domain.Enums;

namespace Online_Shop.Database.Tests
{
    public class OrderManagerTests : DbTest
    {
        OrderManager _orderManager;
        public OrderManagerTests() : base()
        {
            _orderManager = new OrderManager(_dbContext);
        }

        Order CreateValidOrder(string reference) => new Order
        {
            FirstName = "jaroslaw",
            LastName = "kaczynski",
            Address = "wiejska",
            City = "warszawa",
            PostCode = "01-641",
            Email = "j.kaczynski@sejm.gov",
            PhoneNumber = "666666666",
            OrderReference = reference,
            // filler data used for model to be valid,
            // stripe service is responsible for creating its reference
            StripeReference = "xyz"
        };

        [Fact]
        public async Task Create_Valid_Order()
        {
            var testOrder = CreateValidOrder("ref4");

            testOrder.Email = "j.kasdasda@gmail.com";

            var result = await _orderManager.CreateOrder(testOrder);

            Assert.True(result);
            Assert.Contains(_dbContext.Orders, order => order.OrderReference == testOrder.OrderReference);
        }

        [Fact]
        public async Task Try_Create_Invalid_Order()
        {
            var testOrder = CreateValidOrder("ref4");

            testOrder.Email = null;

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _orderManager.CreateOrder(testOrder));
        }

        [Fact]
        public void Get_Existing_Order_By_Reference()
        {
            Assert.NotNull(_orderManager.GetOrderByReference("ref1", order => order));
        }

        [Fact]
        public void Get_Non_Existing_Order_By_Reference()
        {
            Assert.Null(_orderManager.GetOrderByReference("xd", order => order));
        }

        // EntityFramework generates id automatically, and not always going from 0,
        // so to have confidence that given id exist i first get ids of orders in database
        [Fact]
        public void Get_Existing_Order_By_Id()
        {

            Assert.NotNull(_orderManager.GetOrderById(1, order => order));
        }

        [Fact]
        public void Get_Non_Existing_Order_By_Id()
        {

            Assert.Null(_orderManager.GetOrderById(5, order => order));
        }

        [Fact]
        public void Does_Order_Reference_Exist()
        {
            Assert.True(_orderManager.DoesOrderReferenceExist("ref1"));
        }

        [Fact]
        public void Does_Order_Reference_Not_Exist()
        {
            Assert.False(_orderManager.DoesOrderReferenceExist("lol"));
        }

        [Fact]
        public void Get_Orders_By_Status()
        {
            var orders = _orderManager.GetOrdersByStatus(OrderStatus.Pending, order => order).ToList();
            Assert.True(orders.All(order => order.Status == OrderStatus.Pending));
        }

        [Fact]
        public async Task Advance_Order()
        {
            var pendingOrder = _dbContext.Orders.FirstOrDefault(order => order.OrderReference == "ref1");

            var result = await _orderManager.AdvanceOrder(pendingOrder.Id);

            Assert.True(result);
            Assert.True(pendingOrder.Status > OrderStatus.Pending);
        }

        [Fact]
        public async Task Try_Advance_Shipped_Order()
        {
            var shippedOrder = _dbContext.Orders.FirstOrDefault(order => order.Status == OrderStatus.Shipped);

            var result = await _orderManager.AdvanceOrder(shippedOrder.Id);

            Assert.False(result);
            Assert.Equal(OrderStatus.Shipped, shippedOrder.Status);
        }
    }
}

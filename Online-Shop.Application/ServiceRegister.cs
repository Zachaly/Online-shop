using Microsoft.AspNetCore.Http;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Orders;
using Online_Shop.Application.OrdersAdmin;
using Online_Shop.Application.Products;
using Online_Shop.Application.ProductsAdmin;
using Online_Shop.Application.StockAdmin;
using Online_Shop.Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        /// <summary>
        /// Adds depedencies used in this app
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<Online_Shop.Application.Cart.GetOrder>();
            @this.AddTransient<RemoveFromCart>();

            @this.AddTransient<CreateOrder>();
            @this.AddTransient<Online_Shop.Application.Orders.GetOrder>();

            @this.AddTransient<Online_Shop.Application.OrdersAdmin.GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<Online_Shop.Application.Products.GetProduct>();
            @this.AddTransient<Online_Shop.Application.Products.GetProducts>();

            @this.AddTransient<CreateProduct>();
            @this.AddTransient<DeleteProduct>();
            @this.AddTransient<Online_Shop.Application.ProductsAdmin.GetProduct>();
            @this.AddTransient<Online_Shop.Application.ProductsAdmin.GetProducts>();
            @this.AddTransient<UpdateProduct>();

            @this.AddTransient<CreateStock>();
            @this.AddTransient<DeleteStock>();
            @this.AddTransient<GetStock>();
            @this.AddTransient<UpdateStock>();

            @this.AddTransient<CreateManager>();
            @this.AddTransient<GetUsers>();
            @this.AddTransient<DeleteUser>();
            
            return @this;
        }
    }
}

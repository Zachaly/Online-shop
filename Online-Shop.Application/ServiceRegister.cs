using Microsoft.AspNetCore.Http;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Orders;
using Online_Shop.Application.OrdersAdmin;
using Online_Shop.Application.Products;
using Online_Shop.Application.ProductsAdmin;
using Online_Shop.Application.StockAdmin;
using Online_Shop.Application.UsersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<GetCartOrder>();
            @this.AddTransient<RemoveFromCart>();

            @this.AddTransient<CreateOrder>();
            @this.AddTransient<GetOrder>();

            @this.AddTransient<GetAdminOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<GetProduct>();
            @this.AddTransient<GetProducts>();

            @this.AddTransient<CreateProduct>();
            @this.AddTransient<DeleteProduct>();
            @this.AddTransient<GetAdminProduct>();
            @this.AddTransient<GetAdminProducts>();
            @this.AddTransient<UpdateProduct>();

            @this.AddTransient<CreateStock>();
            @this.AddTransient<DeleteStock>();
            @this.AddTransient<GetStock>();
            @this.AddTransient<UpdateStock>();

            @this.AddTransient<CreateUser>();
            @this.AddTransient<GetUsers>();
            @this.AddTransient<DeleteUser>();
            
            return @this;
        }
    }
}

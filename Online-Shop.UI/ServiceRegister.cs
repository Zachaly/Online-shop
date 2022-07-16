using Microsoft.AspNetCore.Http;
using Online_Shop.Application;
using Online_Shop.Application.Cart;
using Online_Shop.Application.Orders;
using Online_Shop.Application.OrdersAdmin;
using Online_Shop.Application.Products;
using Online_Shop.Application.ProductsAdmin;
using Online_Shop.Application.StockAdmin;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        /// <summary>
        /// Adds depedencies used in this app
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);

            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes.
                Where(type => type.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach(var service in services)
            {
                @this.AddTransient(service);
            }

            return @this;
        }
    }
}

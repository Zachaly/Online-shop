using Microsoft.Extensions.DependencyInjection;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.Database;

namespace Online_Shop.UI.Infrastructure
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection AddApplicationInfrastucture(this IServiceCollection @this)
        {
            @this.AddHttpContextAccessor();
            @this.AddTransient<ISessionManager, SessionManager>();
            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IUserManager, UserManager>();

            return @this;
        }
    }
}

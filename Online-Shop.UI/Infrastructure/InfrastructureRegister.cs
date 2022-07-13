using Microsoft.Extensions.DependencyInjection;
using Online_shop.Domain.Infrastructure;
using Online_Shop.Database;
using Online_Shop.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Infrastructure
{
    public static class InfrastructureRegister
    {
        public static IServiceCollection AddApplicationInfrastucture(this IServiceCollection @this)
        {
            @this.AddHttpContextAccessor();
            @this.AddTransient<ISessionManager, SessionManager>();
            @this.AddScoped<IStockManager, StockManager>();

            return @this;
        }
    }
}

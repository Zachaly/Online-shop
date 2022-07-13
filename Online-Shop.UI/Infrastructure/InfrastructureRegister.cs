using Microsoft.Extensions.DependencyInjection;
using Online_Shop.Application.Infrastructure;
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

            return @this;
        }
    }
}

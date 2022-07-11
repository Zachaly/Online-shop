using Online_shop.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Application.OrdersAdmin
{
    public class UpdateOrder
    {
        private AppDbContext _dbContext;

        public UpdateOrder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Execute(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(order => order.Id == id);

            order.Status += 1;

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

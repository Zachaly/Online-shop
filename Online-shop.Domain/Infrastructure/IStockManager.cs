using Online_shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_shop.Domain.Infrastructure
{
    public interface IStockManager
    {
        Stock GetStockWithProduct(int stockId);
        bool EnoughtStock(int stockId, int quantity);
        Task PutStockOnHold(int stockId, string sessionId, int quantity);
        public Task RemoveStockFromHold(int stockId, string sessionId, int quantity);
    }
}

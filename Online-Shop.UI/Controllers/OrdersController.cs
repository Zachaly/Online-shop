using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Shop.Application.OrdersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        private GetOrders _getOrders;
        private GetOrder _gerOrder;
        private UpdateOrder _updateOrder;

        public OrdersController(GetOrders getOrders, GetOrder getOrder, UpdateOrder updateOrder)
        {
            _getOrders = getOrders;
            _gerOrder = getOrder;
            _updateOrder = updateOrder;
        }

        [HttpGet("")]
        public IActionResult GetOrders(int status) => Ok(_getOrders.Execute(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id) => Ok(_gerOrder.Execute(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id) => Ok(await _updateOrder.Execute(id));
    }
}

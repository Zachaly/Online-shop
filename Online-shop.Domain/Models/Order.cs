using Online_shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_shop.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OrderReference { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }

        public string StripeReference { get; set; }
        
        public OrderStatus Status { get; set; }

        public ICollection<OrderStock> OrderStocks { get; set; }
    }
}

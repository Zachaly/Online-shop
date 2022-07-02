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
        public string OrderReference { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

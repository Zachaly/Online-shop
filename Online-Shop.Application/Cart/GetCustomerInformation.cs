using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Get customer information from session
    /// </summary>
    public class GetCustomerInformation
    {
        private ISession _session;

        public GetCustomerInformation(IHttpContextAccessor httpAccessor)
        {
            _session = httpAccessor.HttpContext.Session;
        }

        public Response Execute()
        {
            var customerInfoString = _session.GetString("customer-info");

            if (string.IsNullOrEmpty(customerInfoString))
            {
                return null;
            }

            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);

            return new Response
            {
                FirstName = customerInfo.FirstName,
                Address = customerInfo.Address,
                City = customerInfo.City,
                Email = customerInfo.Email,
                LastName = customerInfo.LastName,
                PhoneNumber = customerInfo.PhoneNumber,
                PostCode = customerInfo.PostCode
            };
        }

        public class Response
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            public string Address { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
        }
    }
}

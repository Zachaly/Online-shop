using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Online_Shop.Application.Cart
{   
    /// <summary>
    /// Adds customer information to session
    /// </summary>
    public class AddCustomerInformation
    {
        private ISession _session;

        
        public AddCustomerInformation(IHttpContextAccessor httpAccessor)
        {
            _session = httpAccessor.HttpContext.Session;
        }

        public void Execute(Request request)
        {
            var customerInformation = new CustomerInformation
            {
                FirstName = request.FirstName,
                Address = request.Address,
                City = request.City,
                Email = request.Email,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PostCode = request.PostCode
            };
            
            var customerInfo = JsonConvert.SerializeObject(customerInformation);

            _session.SetString("customer-info", customerInfo);
        }

        public class Request
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

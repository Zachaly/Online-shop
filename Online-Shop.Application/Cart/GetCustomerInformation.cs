using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Online_shop.Domain.Models;
using Online_Shop.Domain.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Get customer information from session
    /// </summary>
    public class GetCustomerInformation
    {
        private ISessionManager _sessionManager;

        public GetCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Response Execute()
        {
            var customerInfo = _sessionManager.GetCustomerInformation();

            if (customerInfo is null)
                return null;

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

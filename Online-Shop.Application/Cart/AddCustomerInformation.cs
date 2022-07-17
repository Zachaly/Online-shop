using Online_Shop.Domain.Models;
using Online_Shop.Domain.Infrastructure;

namespace Online_Shop.Application.Cart
{
    /// <summary>
    /// Adds customer information to session
    /// </summary>
    [Service]
    public class AddCustomerInformation
    {
        private readonly ISessionManager _sessionManager;

        public AddCustomerInformation(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
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
            
            _sessionManager.AddCustomerInformation(customerInformation);
        }

        public class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}

using FluentValidation;
using Online_Shop.Application.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.UI.Validators
{
    public class AddCustomerInformationRequestValidator 
        : AbstractValidator<AddCustomerInformation.Request>
    {
        public AddCustomerInformationRequestValidator()
        {
            RuleFor(info => info.FirstName).NotEmpty();
            RuleFor(info => info.LastName).NotEmpty();
            RuleFor(info => info.Address).NotEmpty();
            RuleFor(info => info.City).NotEmpty();
            RuleFor(info => info.PostCode).NotEmpty();
            RuleFor(info => info.Email).EmailAddress().NotEmpty();
            RuleFor(info => info.PhoneNumber).MinimumLength(9);
        }
    }
}

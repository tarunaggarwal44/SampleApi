using FluentValidation;
using Sample.Api.Common.Contracts;
using Sample.Api.Common.Contracts.Constants;
using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using System.Text.RegularExpressions;

namespace Sample.Api.Customers.Business.Validations
{
   
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerValidator(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(AppBusinessMessages.FirstNameInValid).Must(isNameValid).WithMessage(AppBusinessMessages.FirstNameInValid);
            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(AppBusinessMessages.LastNameInValid).Must(isNameValid).WithMessage(AppBusinessMessages.LastNameInValid);
            RuleFor(x => x.Gender).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(AppBusinessMessages.GenderNotValid).Must(isNameValid).WithMessage(AppBusinessMessages.GenderNotValid);
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(AppBusinessMessages.EmailIdNotValid).Must(isEmailValid).WithMessage(AppBusinessMessages.EmailIdNotValid);
        }
   

        private bool isNameValid(string name)
        {
            return Regex.IsMatch(name, Regexes.Name);
        }

        private bool isEmailValid(string email)
        {
            return Regex.IsMatch(email, Regexes.Email);
        }
    }
}

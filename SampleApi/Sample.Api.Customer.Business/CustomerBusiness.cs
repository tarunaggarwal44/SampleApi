﻿using FluentValidation.Results;
using MediatR;
using Sample.Api.Common.Contracts;
using Sample.Api.Common.Contracts.Constants;
using Sample.Api.Common.Contracts.Events;
using Sample.Api.Customer.Contracts;
using Sample.Api.Customer.Contracts.Events;
using Sample.Api.Customers.Business.Validations;
using Sample.Api.Customers.Contracts;
using Sample.Api.Customers.Contracts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Customers.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMediator mediator;
        public CustomerBusiness(ICustomerRepository customerRepository, IMediator mediator)
        {
            this.customerRepository = customerRepository;
            this.mediator = mediator;
        }

        public async Task<Response<string>> CreateCustomer(CustomerModel customerModel)
        {
            BaseDomainEvent baseDomainEvent = new CustomerCreatedDomainEvent(customerModel);
            BaseDomainEvent baseDomainEvent1 = new CustomerCreatedDomainEvent1(customerModel);
            await this.mediator.Publish(baseDomainEvent);
            await this.mediator.Publish(baseDomainEvent1);

            customerModel.CreateCustomer();
            //await this.mediator.Publish(customerModel.Events[0]);
            //await this.mediator.Publish(customerModel.Events[0]);

            var validator = new CustomerValidator();
            ValidationResult results = validator.Validate(customerModel);

            if (results.IsValid)
            {
                var id = await customerRepository.CreateCustomer(customerModel);
                
                

                return id;
            }

            else
                return new Response<string>() { ResultType = ResultType.ValidationError, Messages = results.Errors.Select(a => a.ErrorMessage).ToList() };
        }

        public async Task<Response<bool>> DeleteCustomer(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new Response<bool>() { ResultType = ResultType.ValidationError, Messages = new List<string>() { AppBusinessMessages.EmailIdNotValid } };
            }

            var customerExists = await CheckIfCustomerExists(email);
            if (customerExists)
            {
                return await this.customerRepository.DeleteCustomer(email);
            }
            return new Response<bool>() { ResultType = ResultType.Error, Messages = new List<string>() { AppBusinessMessages.CustomerNotFound } };
        }

        public async Task<Response<IEnumerable<CustomerModel>>> GetAllCustomers()
        {
            return await this.customerRepository.GetAllCustomers();
        }

        public async Task<Response<CustomerModel>> GetCustomer(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new Response<CustomerModel>() { ResultType = ResultType.ValidationError, Messages = new List<string>() { AppBusinessMessages.EmailIdNotValid } };
            }

            return await customerRepository.GetCustomer(email);
        }

        public async Task<Response<bool>> UpdateCustomer(CustomerModel customerModel)
        {
            var customerExists = await CheckIfCustomerExists(customerModel.Email);
            if (customerExists)
            {
                return await this.customerRepository.UpdateCustomer(customerModel);
            }

            return new Response<bool>() { ResultType = ResultType.Error, Messages = new List<string>() { AppBusinessMessages.CustomerNotFound } };
        }

        private async Task<bool> CheckIfCustomerExists(string email)
        {
            var customer = await customerRepository.GetCustomer(email);
            if (customer.HasResult)
                return true;

            return false;
        }
    }
}

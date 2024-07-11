using Repository.Customers;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<string> CreateCustomer(CreateCustomerRequestModel customer)
        {
            return await _customerRepository.CreateCustomer(customer);
        }

        public async Task<CustomerResponseModel> GetCustomerByEmail(string email)
        {
            return await _customerRepository.GetCustomerByEmail(email);
        }

        public async Task<CustomerResponseModel> GetCustomerByID(int id)
        {
            return await _customerRepository.GetCustomerByID(id);
        }

        public async Task<string> UpdateCustomer(UpdateCustomerRequestModel customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
        }
    }
}

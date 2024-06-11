using Repository.Customers;
using Repository.Models.RequestModels;
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
    }
}

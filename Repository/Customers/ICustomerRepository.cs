using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Customers
{
    public interface ICustomerRepository
    {
        Task<string> CreateCustomer(CreateCustomerRequestModel customer);
        Task<CustomerResponseModel> GetCustomerByID(int id);
        Task<string> UpdateCustomer(UpdateCustomerRequestModel customer);
        Task<CustomerResponseModel> GetCustomerByEmail(string email);
    }
}

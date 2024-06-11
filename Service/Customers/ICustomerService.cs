using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Customers
{
    public interface ICustomerService
    {
        Task<string> CreateCustomer(CreateCustomerRequestModel customer);
        Task<CustomerResponseModel> GetCustomerByID(int id);
        Task<string> UpdateCustomer(UpdateCustomerRequestModel customer);
    }
}

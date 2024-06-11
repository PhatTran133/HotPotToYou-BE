using Repository.Models.RequestModels;
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
    }
}

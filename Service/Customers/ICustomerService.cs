using Repository.Models.RequestModels;
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
    }
}

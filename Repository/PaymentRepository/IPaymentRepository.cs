using Repository.Models.RequestModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task<string> AddAsync(PaymentModel model);
        Task<string> UpdateAsync(PaymentRequest model);
        Task<string> DeleteAsync(int id);
    }
}

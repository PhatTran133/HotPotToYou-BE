using Repository.Models.RequestModels.Payment;
using Repository.PaymentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Payment
{
    public class PaymentService :IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<string> AddAsync(PaymentModel model)
        {
            return await _paymentRepository.AddAsync(model);
        }

        public async Task<string> UpdateAsync(PaymentRequest model)
        {
            return await _paymentRepository.UpdateAsync(model);
        }

        public async Task<string> DeleteAsync(int id)
        {
            return await _paymentRepository.DeleteAsync(id);
        }
    }
}

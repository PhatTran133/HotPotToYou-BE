using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels.Payment;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PaymentRepository
{
    public class PaymentRepository :IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public PaymentRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> AddAsync(PaymentModel model)
        {
            var checkPayMent = await _context.Payment.AnyAsync(x => x.Name == model.Name && x.DeleteDate == null);
            if (checkPayMent) 
                throw new Exception("Payment is existed");

            var newPayment = new PaymentEntity()
            {
                Name = model.Name,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };
            _context.Payment.Add(newPayment);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Successfully";
            else
                return "Create Failed";
        }

        public async Task<string> UpdateAsync(PaymentRequest model)
        {
            var payment = await _context.Payment.SingleOrDefaultAsync(x => x.ID == model.ID && x.DeleteDate == null);

            if (payment == null) return "Payment not existed";

            payment.Name = model.Name;
            payment.UpdateByID = _currentUserService.UserId;
            payment.UpdateDate = DateTime.Now;

            _context.Payment.Update(payment);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var payment = await _context.Payment.SingleOrDefaultAsync(x => x.ID == id);

            if (payment == null) return "Payment not existed";
            else
            {
                payment.DeleteByID = _currentUserService.UserId;
                payment.DeleteDate = DateTime.Now;
                _context.Payment.Update(payment);
                if (await _context.SaveChangesAsync() > 0)
                    return "Delete Successfully";
                else
                    return "Delete Failed";
            }
        }
    }
}

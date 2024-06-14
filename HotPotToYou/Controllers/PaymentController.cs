using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.Payment;
using Service.Payment;

namespace HotPotToYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(PaymentModel model)
        {
            var result = await _paymentService.AddAsync(model);
            if (result == "Create Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentRequest model)
        {
            var result = await _paymentService.UpdateAsync(model);
            if (result == "Update Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _paymentService.DeleteAsync(id);
            if (result == "Delete Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}

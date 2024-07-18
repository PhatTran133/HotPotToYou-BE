using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.Payment;
using Service.Payment;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("payment")]
        public async Task<IActionResult> Add(PaymentModel model)
        {
            try
            {
                var result = await _paymentService.AddAsync(model);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("payment")]
        public async Task<IActionResult> Update(PaymentRequest model)
        {
            try
            {
                var result = await _paymentService.UpdateAsync(model);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            
           
        }

        [HttpDelete("payment")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _paymentService.DeleteAsync(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
           
        }
    }
}

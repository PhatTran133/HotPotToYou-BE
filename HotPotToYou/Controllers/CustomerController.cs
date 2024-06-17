using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
using Repository.Models.ResponseModels;
using Service.Customers;
using System.Net.Mime;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [AllowAnonymous]
        [HttpPost("customer/register")]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateCustomer(
            [FromBody] CreateCustomerRequestModel customer)
        {
            try
            {
                var result = await _customerService.CreateCustomer(customer);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            
        }

        [HttpGet("customer/get-customer-by-id")]
        public async Task<ActionResult<JsonResponse<CustomerResponseModel>>> GetCustomerByID(int id)
        {
            try
            {
                var result = await _customerService.GetCustomerByID(id);
                return Ok(new JsonResponse<CustomerResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
            
        }

        [HttpPut("customer")]
        public async Task<ActionResult<JsonResponse<Guid>>> UpdateCustomer(
            [FromBody] UpdateCustomerRequestModel customer)
        {
            try
            {
                var result = await _customerService.UpdateCustomer(customer);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
    }
}

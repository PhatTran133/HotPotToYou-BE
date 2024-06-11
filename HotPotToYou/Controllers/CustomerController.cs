using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels;
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

        [HttpPost("customer/register")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<Guid>>> CreateCustomer(
            [FromBody] CreateCustomerRequestModel customer)
        {
            var result = await _customerService.CreateCustomer(customer);
            return Ok(new JsonResponse<string>(result));
        }
    }
}

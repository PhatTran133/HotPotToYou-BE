using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.Order;
using Repository.Models.ResponseModels;
using Service.Order;
using System.Collections.Generic;

namespace HotPotToYou.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel order)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _orderService.CreateOrder(order);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("order")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequestModel order)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != order.ID)
                    return BadRequest("Order ID mismatch");

                var result = await _orderService.UpdateOrder(order);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpDelete("order")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _orderService.DeleteOrder(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("get-wait-for-pay-orders")]
        public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetWaitForPayOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            try
            {
                var orders = await _orderService.GetWaitForPayOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
                return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
        [HttpGet("get-pending-orders")]
        public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetPendingOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            try
            {
                var orders = await _orderService.GetPendingOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
                return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [HttpGet("get-in-process-orders")]
        public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetInProcessOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            try
            {
                var orders = await _orderService.GetInProcessOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
                return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [HttpGet("get-delivered-orders")]
        public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetDeliveredOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            try
            {
                var orders = await _orderService.GetDeliveredOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
                return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [HttpGet("get-canceled-orders")]
        public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetCanceledOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            try
            {
                var orders = await _orderService.GetCanceledOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
                return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("get-order-by-id")]
        public async Task<ActionResult<JsonResponse<OrderDetailResponseModel>>> GetOrderByID(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByID(id);
                if (order == null)
                    return NotFound("Order not found");

                return Ok(new JsonResponse<OrderDetailResponseModel>(order));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("update-order-to-in-process")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToInProcess(int id)
        {
            try
            {
                var result = await _orderService.UpdateOrderToInProcess(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("update-order-to-delivered")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToDelivered(int id)
        {
            try
            {
                var result = await _orderService.UpdateOrderToDelivered(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("update-order-to-canceled")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToCanceled(int id)
        {
            try
            {
                var result = await _orderService.UpdateOrderToCanceled(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
    }
}

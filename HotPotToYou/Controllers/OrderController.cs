﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models.RequestModels.Order;
using Service.Order;

namespace HotPotToYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderService.CreateOrder(order);
            if (result == "Create Order Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequestModel order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != order.ID)
                return BadRequest("Order ID mismatch");

            var result = await _orderService.UpdateOrder(order);
            if (result == "Update Order Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result == "Delete Order Successfully")
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex = 1, int pageSize = 10)
        {
            var orders = await _orderService.GetOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByID(int id)
        {
            var order = await _orderService.GetOrderByID(id);
            if (order == null)
                return NotFound("Order not found");

            return Ok(order);
        }
    }
}
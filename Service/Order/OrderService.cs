using AutoMapper;
using Repository.Models.RequestModels.Order;
using Repository.Models.ResponseModels;
using Repository.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateOrder(CreateOrderRequestModel order)
        {
            return await _orderRepository.CreateOrder(order);
        }

        public async Task<string> UpdateOrder(UpdateOrderRequestModel order)
        {
            return await _orderRepository.UpdateOrder(order);
        }

        public async Task<string> DeleteOrder(int id)
        {
            return await _orderRepository.DeleteOrder(id);
        }


        public async Task<OrderDetailResponseModel> GetOrderByID(int id)
        {
            return await _orderRepository.GetOrderByID(id);
        }

        public async Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            return await _orderRepository.GetWaitForPayOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }

        public async Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            return await _orderRepository.GetPendingOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }

        public async Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            return await _orderRepository.GetInProcessOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }

        public async Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            return await _orderRepository.GetDeliveredOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }
        public async Task<List<OrderResponseModel>> GetCanceledOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            return await _orderRepository.GetCanceledOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }

        public async Task<string> UpdateOrderAfterPaying()
        {
            return await _orderRepository.UpdateOrderAfterPaying();
        }

        public async Task<string> UpdateOrderToInProcess(int id)
        {
            return await _orderRepository.UpdateOrderToInProcess(id);
        }

        public async Task<string> UpdateOrderToDelivered(int id)
        {
            return await _orderRepository.UpdateOrderToDelivered(id);
        }

        public async Task<string> UpdateOrderToCanceled(int id)
        {
            return await _orderRepository.UpdateOrderToCanceled(id);
        }

        
    }
}

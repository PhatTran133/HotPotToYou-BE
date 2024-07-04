using AutoMapper;
using Repository.Models.RequestModels.Order;
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
            // Bạn có thể thêm các logic nghiệp vụ tại đây trước khi gọi repository
            return await _orderRepository.CreateOrder(order);
        }

        public async Task<string> UpdateOrder(UpdateOrderRequestModel order)
        {
            // Bạn có thể thêm các logic nghiệp vụ tại đây trước khi gọi repository
            return await _orderRepository.UpdateOrder(order);
        }

        public async Task<string> DeleteOrder(int id)
        {
            // Bạn có thể thêm các logic nghiệp vụ tại đây trước khi gọi repository
            return await _orderRepository.DeleteOrder(id);
        }

        public async Task<List<OrderResponseModel>> GetOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            // Bạn có thể thêm các logic nghiệp vụ tại đây trước khi gọi repository
            return await _orderRepository.GetOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        }

        public async Task<OrderResponseModel> GetOrderByID(int id)
        {
            // Bạn có thể thêm các logic nghiệp vụ tại đây trước khi gọi repository
            return await _orderRepository.GetOrderByID(id);
        }
    }
}

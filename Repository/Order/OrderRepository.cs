using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels.Order;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public OrderRepository(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<string> CreateOrder(CreateOrderRequestModel order)
        {
            var newOrder = new OrderEntity()
            {
                PurchaseDate = order.PurchaseDate,
                CustomerID = order.CustomerID,
                Adress = order.Adress,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                PaymentID = order.PaymentID,
                PaymentStatus = order.PaymentStatus,
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
                // Add additional properties if needed
            };

            _context.Order.Add(newOrder);
            if (await _context.SaveChangesAsync() > 0)
                return "Create Order Successfully";
            else
                return "Create Order Failed";
        }

        public async Task<string> UpdateOrder(UpdateOrderRequestModel order)
        {
            var orderEntity = await _context.Order.SingleOrDefaultAsync(x => x.ID == order.ID);
            if (orderEntity == null)
                throw new InvalidDataException("Order is not found");

            orderEntity.PurchaseDate = order.PurchaseDate;
            orderEntity.CustomerID = order.CustomerID;
            orderEntity.Adress = order.Adress;
            orderEntity.TotalPrice = order.TotalPrice;
            orderEntity.Status = order.Status;
            orderEntity.PaymentID = order.PaymentID;
            orderEntity.PaymentStatus = order.PaymentStatus;
            // Update additional properties if needed

            _context.Order.Update(orderEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }

        public async Task<string> DeleteOrder(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(x => x.ID == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            _context.Order.Remove(order);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Order Successfully";
            else
                return "Delete Order Failed";
        }

        public async Task<List<OrderResponseModel>> GetOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Where(x => x.Status == true);

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Adress.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.PurchaseDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.PurchaseDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.PurchaseDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.PurchaseDate);
                }
            }

            var paginatedOrders = PaginatedList<OrderEntity>.Create(orders, pageIndex, pageSize);

            return _mapper.Map<List<OrderResponseModel>>(paginatedOrders);
        }

        public async Task<OrderResponseModel> GetOrderByID(int id)
        {
            var order = await _context.Order.Include(x => x.Customer).SingleOrDefaultAsync(x => x.ID == id);
            if (order == null)
                throw new Exception("Order is not found");

            return _mapper.Map<OrderResponseModel>(order);
        }
    
    }
}

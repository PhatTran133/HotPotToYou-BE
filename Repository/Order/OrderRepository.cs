using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DbContexts;
using Repository.Entity;
using Repository.Models.RequestModels.Order;
using Repository.Models.ResponseModels;
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

        public async Task<string> CreateOrder(CreateOrderRequestModel orderRequest)
        {
            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
            {
                throw new ArgumentException("Invalid order data.");
            }

            var order = new OrderEntity()
            {
                PurchaseDate = orderRequest.PurchaseDate,
                CustomerID = orderRequest.CustomerID,
                Adress = orderRequest.Adress,
                TotalPrice = orderRequest.TotalPrice,
                Status = true,
                PaymentID = orderRequest.PaymentID,
                PaymentStatus = "Chưa hoàn thành",
                CreateByID = _currentUserService.UserId,
                CreateDate = DateTime.Now
            };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in orderRequest.Items)
            {
                if (item.Type == "hotpot")
                {
                    var hotpotDetail = new HotPotPackageEntity()
                    {
                        OrderId = order.ID,
                        HotPotID = item.Id,
                        Quantity = item.Quantity,
                        Total = item.Total
                    };
                    _context.HotPotPackage.Add(hotpotDetail);
                }
                else if (item.Type == "utensil")
                {
                    if (item.IsPackage)
                    {
                        var utensilDetail = new OrderUtensilEntity()
                        {
                            OrderID = order.ID,
                            UtensilID = 0,
                            UtensilPackageID = item.Id,
                            Quantity = item.Quantity,
                            Total = item.Total
                        };
                        _context.OrderUtensil.Add(utensilDetail);
                    }
                    else
                    {
                        var utensilDetail = new OrderUtensilEntity
                        {
                            OrderID = order.ID,
                            UtensilID = item.Id,
                            UtensilPackageID = 0,
                            Quantity = item.Quantity,
                            Total = item.Total
                        };
                        _context.OrderUtensil.Add(utensilDetail);
                    }
                }
            }

            var orderActivityID = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đang chờ thanh toán"));

            var orderActivity = new OrderActivityEntity
            {
                OrderID = order.ID,
                ActivityTypeID = orderActivityID.ID,
                DateTime = DateTime.Now,
            };

            _context.OrderActivity.Add(orderActivity);

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
        public async Task<string> UpdateOrderAfterPaying()
        {
            var userID = Convert.ToInt32(_currentUserService.UserId);
            var order = await _context.Order.FirstOrDefaultAsync(x => x.CustomerID == userID);
            var activity = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đang chờ xác nhận"));
            order.OrderActivity.ActivityTypeID = activity.ID;

            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }
        public async Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy,
            DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Include(x => x.Payment).Where(x => x.Status == true && x.OrderActivity.ActivityType.Name.Equals("Đang chờ thanh toán"));

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
        public async Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Include(x => x.Payment).Where(x => x.Status == true && x.OrderActivity.ActivityType.Name.Equals("Đang chờ xác nhận"));

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
        public async Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Include(x => x.Payment).Where(x => x.Status == true && x.OrderActivity.ActivityType.Name.Equals("Đang vận chuyển"));

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
        public async Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Include(x => x.Payment).Where(x => x.Status == true && x.OrderActivity.ActivityType.Name.Equals("Đã giao hàng"));

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
        public async Task<List<OrderResponseModel>> GetCanceledOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<OrderEntity> orders = _context.Order.Include(x => x.Customer).Include(x => x.Payment).Where(x => x.Status == true && x.OrderActivity.ActivityType.Name.Equals("Đã hủy"));

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
        public async Task<OrderDetailResponseModel> GetOrderByID(int id)
        {
            var order = await _context.Order
                .Include(x => x.Customer)
                .Include(x => x.Payment)
                .Include(x => x.HotPotPackages) // Nạp các món hotpot
                    .ThenInclude(hp => hp.HotPot)
                .Include(x => x.OrderUtensils) // Nạp các dụng cụ
                    .ThenInclude(ou => ou.Utensil)
                .SingleOrDefaultAsync(x => x.ID == id);

            if (order == null)
                throw new Exception("Order is not found");

            var orderResponse = new OrderDetailResponseModel
            {
                PurchaseDate = order.PurchaseDate,
                Email = order.Customer.Email,
                Adress = order.Adress,
                TotalPrice = order.TotalPrice,
                Payment = order.Payment.Name,
                PaymentStatus = order.PaymentStatus,
                Items = new List<OrderItemResponse>(),
                Activities = new List<Activity>()
            };

            foreach (var hotpotPackage in order.HotPotPackages)
            {
                orderResponse.Items.Add(new OrderItemResponse
                {
                    Type = "Hot Pot",
                    Id = hotpotPackage.HotPotID,
                    Quantity = hotpotPackage.Quantity,
                    Total = hotpotPackage.Total,
                });
            }

            foreach (var utensilDetail in order.OrderUtensils)
            {
                if (utensilDetail.UtensilID != 0)
                {
                    orderResponse.Items.Add(new OrderItemResponse
                    {
                        Type = "Utensil",
                        Id = utensilDetail.UtensilID,
                        Quantity = utensilDetail.Quantity,
                        Total = utensilDetail.Total,
                    });
                }
                else if (utensilDetail.UtensilPackageID != 0)
                {
                    orderResponse.Items.Add(new OrderItemResponse
                    {
                        Type = "Pot",
                        Id = utensilDetail.UtensilPackageID,
                        Quantity = utensilDetail.Quantity,
                        Total = utensilDetail.Total,
                    });
                }
            }

            var orderActivities = await _context.OrderActivity
                .Include(x => x.ActivityType)
                .Where(x => x.OrderID == order.ID)
                .ToListAsync();

            foreach (var orderActivity in orderActivities)
            {
                orderResponse.Activities.Add(new Activity
                {
                    Action = orderActivity.ActivityType.Name,
                    DateTime = orderActivity.DateTime
                });
            }

            return _mapper.Map<OrderDetailResponseModel>(orderResponse);
        }

        public async Task<string> UpdateOrderToInProcess(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(x => x.ID == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            var activity = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đang vận chuyển"));

            var orderActivity = new OrderActivityEntity
            {
                OrderID = order.ID,
                ActivityTypeID = activity.ID,
                DateTime = DateTime.Now
            };

            _context.OrderActivity.Add(orderActivity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }

        public async Task<string> UpdateOrderToDelivered(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(x => x.ID == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            var activity = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đã giao hàng"));
            var orderActivity = new OrderActivityEntity
            {
                OrderID = order.ID,
                ActivityTypeID = activity.ID,
                DateTime = DateTime.Now
            };

            _context.OrderActivity.Add(orderActivity);

            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }

        public async Task<string> UpdateOrderToCanceled(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(x => x.ID == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            var activity = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đã hủy"));
            var orderActivity = new OrderActivityEntity
            {
                OrderID = order.ID,
                ActivityTypeID = activity.ID,
                DateTime = DateTime.Now
            };

            _context.OrderActivity.Add(orderActivity);

            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }
    }
}

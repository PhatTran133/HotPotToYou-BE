﻿using Repository.Models.RequestModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Order
{
    public interface IOrderService
    {
        Task<string> CreateOrder(CreateOrderRequestModel order);
        Task<string> UpdateOrder(UpdateOrderRequestModel order);
        Task<string> DeleteOrder(int id);
        Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy,
          DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy,
          DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy,
           DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<OrderResponseModel> GetOrderByID(int id);
    }
}

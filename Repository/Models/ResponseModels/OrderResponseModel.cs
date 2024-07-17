using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.ResponseModels
{
    public class OrderResponseModel
    {
        public DateTime PurchaseDate { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public string Payment { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class OrderDetailResponseModel
    {
        public DateTime PurchaseDate { get; set; }
        public int CustomerID { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public int PaymentID { get; set; }
        public string PaymentStatus { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }

    public class OrderItemResponse
    {

        public string Type { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool IsPackage { get; set; }
    }
}

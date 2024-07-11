using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.RequestModels.Order
{
    public class CreateOrderRequestModel
    {
        public DateTime PurchaseDate { get; set; }
        public int CustomerID { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }
        public int PaymentID { get; set; }
        public string PaymentStatus { get; set; }
    }
    public class UpdateOrderRequestModel : CreateOrderRequestModel
    {
        public int ID { get; set; }
    }
}

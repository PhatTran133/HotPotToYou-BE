using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.RequestModels.Order
{
    public class OrderResponseModel
    {
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }
        public string PaymentStatus { get; set; }
    }
}

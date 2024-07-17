using Repository.Entity.Base;
using Repository.Entity.ConfigTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    [Table("Order")]
    public class OrderEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public virtual CustomerEntity Customer { get; set; }
        public string Adress { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }
        public int PaymentID { get; set; }
        [ForeignKey(nameof(PaymentID))]
        public virtual PaymentEntity Payment { get; set; }
        public string PaymentStatus { get; set; }
        public string? OrderCode { get; set; }

        public virtual List<HotPotPackageEntity> HotPotPackages { get; set; }
        public virtual List<OrderUtensilEntity> OrderUtensils { get; set; }
        public virtual OrderActivityEntity OrderActivity { get; set; }


    }
}

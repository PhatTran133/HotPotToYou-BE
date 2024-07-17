using Repository.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    [Table("OrderUtensil")]
    public class OrderUtensilEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int OrderID { get; set; }
        [ForeignKey(nameof(OrderID))]
        public virtual OrderEntity Order { get; set; }
        public int? UtensilPackageID { get; set; }
        [ForeignKey(nameof(UtensilPackageID))]
        public virtual UtensilPackageEntity UtensilPackage { get; set; }
        public int? UtensilID { get; set; }
        [ForeignKey(nameof(UtensilID))]
        public virtual UtensilEntity Utensil { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Total { get; set; }

    }
}

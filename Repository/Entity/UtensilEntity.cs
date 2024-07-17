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
    [Table("Utensil")]
    public class UtensilEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }
        public string Type { get; set; }

        public virtual HotPotUtensilTypeEntity HotPotUtensilType { get; set; }
        public virtual UtensilDetailEntity UtensilDetail { get; set; }
        public virtual OrderUtensilEntity OrderUtensil { get; set; }
    }
}

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
    [Table("HotPot")]
    public class HotPotEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }
        public int FlavorID { get; set; }
        [ForeignKey(nameof(FlavorID))]
        public virtual HotPotFlavorEntity HotPotFlavor { get; set; }

        public int TypeID { get; set; }
        [ForeignKey(nameof(TypeID))]
        public virtual HotPotTypeEntity HotPotType { get; set; }
        public virtual HotPotPackageEntity HotPotPackage { get; set; }
        public virtual HotPotIngredientEntity HotPotIngredient { get; set; }
    }
}

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
    [Table("HotPotIngredient")]
    public class HotPotIngredientEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int HotpotID { get; set; }
        [ForeignKey(nameof(HotpotID))]
        public virtual HotPotEntity HotPot { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public virtual IngredientEntity Ingredient { get; set; }
    }
}

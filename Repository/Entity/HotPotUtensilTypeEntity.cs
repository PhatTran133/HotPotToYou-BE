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
    [Table("HotPotUtensilType")]

    public class HotPotUtensilTypeEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int HotPotTypeID { get; set; }
        [ForeignKey(nameof(HotPotTypeID))]
        public virtual HotPotTypeEntity HotPotType { get; set; }
        public int UtensilID { get; set; }
        [ForeignKey(nameof(UtensilID))]
        public virtual UtensilEntity Utensil { get; set; }
    }
}

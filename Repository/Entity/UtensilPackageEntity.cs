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
    [Table("UntensilPackage")]
    public class UtensilPackageEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }

        public virtual OrderUtensilEntity OrderUtensil { get; set; }
        public virtual UtensilDetailEntity UtensilDetail { get; set; }
    }
}

using Repository.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.ConfigTable
{
    [Table("HotPotType")]
    public class HotPotTypeEntity : CodeTableBase
    {
        public virtual ICollection<HotPotEntity> HotPot { get; set; }
        public virtual HotPotUtensilTypeEntity HotPotUtensilType { get; set; }
    }
}

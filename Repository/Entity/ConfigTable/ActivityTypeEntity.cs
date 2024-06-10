using Repository.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.ConfigTable
{
    [Table("ActivityType")]
    public class ActivityTypeEntity : CodeTableBase
    {
        public virtual OrderActivityEntity OrderActivity { get; set; }
    }
}

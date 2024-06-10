using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Base
{
    public abstract class EntityBase
    {
        public string? CreateByID { get; set; }
        public DateTime? CreateDate { get; set; }

        public string? UpdateByID { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string? DeleteByID { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity.Base
{
    public abstract class CodeTableBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public string? CreateByID { get; set; }
        public DateTime? CreateDate { get; set; }

        public string? UpdateByID { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string? DeleteByID { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}

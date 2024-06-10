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
    [Table("User")]
    public class UserEntity : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public int RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public virtual RoleEntity Role { get; set; }

        public virtual CustomerEntity Customer { get; set; }
    }
}

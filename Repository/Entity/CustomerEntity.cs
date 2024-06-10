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
    [Table("Customer")]
    public class CustomerEntity : EntityBase
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime YearOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual UserEntity User { get; set; }
        public virtual OrderEntity Order { get; set; }
    }
}

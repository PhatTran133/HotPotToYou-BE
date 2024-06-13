using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.ResponseModels
{
    public class CustomerResponseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime YearOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
    }

    
}

using AutoMapper;
using Repository.Entity;
using Repository.Entity.ConfigTable;
using Repository.Models.RequestModels;
using Repository.Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.ResponseModels
{
    public class UserResponseModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }

    public class LoginResponseModel 
    {
        public string Email { get; set; }
        public int ID { get; set; }
        public string Role { get; set; }

    }
}

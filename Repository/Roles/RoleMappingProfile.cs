using AutoMapper;
using Repository.Entity;
using Repository.Entity.ConfigTable;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Roles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleEntity, RoleResponseModel>();
        }
    }
}

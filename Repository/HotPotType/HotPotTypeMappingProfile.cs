using AutoMapper;
using Repository.Entity;
using Repository.Entity.ConfigTable;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPotType
{
    public class HotPotTypeMappingProfile :Profile
    {
        public HotPotTypeMappingProfile()
        {
            CreateMap<HotPotTypeEntity, HotPotTypeResponseModel>();
        }
    }
}

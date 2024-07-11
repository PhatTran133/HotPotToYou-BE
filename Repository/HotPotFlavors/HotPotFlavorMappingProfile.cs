using AutoMapper;
using Repository.Entity.ConfigTable;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPotFlavors
{
    public class HotPotFlavorMappingProfile : Profile
    {
        public HotPotFlavorMappingProfile()
        {
            CreateMap<HotPotFlavorEntity, HotPotFlavorResponseModel>();
        }
    }
}

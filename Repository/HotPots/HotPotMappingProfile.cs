using AutoMapper;
using Repository.Entity;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HotPots
{
    public class HotPotMappingProfile : Profile
    {
        public HotPotMappingProfile() 
        {
            CreateMap<HotPotEntity, HotPotResponseModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.HotPotType.Name));
        }
    }
}

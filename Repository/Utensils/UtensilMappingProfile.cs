using AutoMapper;
using Repository.Entity;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utensils
{
    public class UtensilMappingProfile : Profile
    {
        public UtensilMappingProfile()
        {
            CreateMap<UtensilEntity, UtensilResponseModel>();
        }
    }
}

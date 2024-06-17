using AutoMapper;
using Repository.Entity.ConfigTable;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IngredientGroupRepository
{
    public class IngredientGroupMappingProfile : Profile
    {
        public IngredientGroupMappingProfile()
        {
            CreateMap<IngredientGroupEntity, IngredientGroupResponseModel>();
        }
    }
}

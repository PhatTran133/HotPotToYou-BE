using AutoMapper;
using Repository.Entity;
using Repository.Entity.ConfigTable;
using Repository.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Order
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderEntity, OrderResponseModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name));

        }
    }
}

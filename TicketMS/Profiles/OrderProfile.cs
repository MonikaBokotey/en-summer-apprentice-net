using AutoMapper;
using TicketMS.Model.Dto;
using TicketMS.Models;
using TicketMS.Models.Dto;

namespace TicketMS.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderPatchDto>().ReverseMap();
        }
    }
}

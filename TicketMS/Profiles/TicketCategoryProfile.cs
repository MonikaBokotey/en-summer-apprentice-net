using TicketMS.Model.Dto;
using TicketMS.Models.Dto;
using TicketMS.Models;
using AutoMapper;

namespace TicketMS.Profiles
{
    public class TicketCategoryProfile : Profile
    {
        public TicketCategoryProfile()
        {
            CreateMap<TicketCategory, TicketCategoryDto>().ReverseMap();
       
        }

      
    }
}

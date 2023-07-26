using AutoMapper;
using TicketMS.Model.Dto;
using TicketMS.Models;
using TicketMS.Models.Dto;

namespace TicketMS.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, EventPatchDto>().ReverseMap();
        }
    }
}

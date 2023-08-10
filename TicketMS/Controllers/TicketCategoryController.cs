using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TicketMS.Model.Dto;
using TicketMS.Models;
using TicketMS.Models.Dto;
using TicketMS.Repositories;


namespace TicketMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketCategoryController : ControllerBase
    {
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public TicketCategoryController(ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public int GetByOrderId(int event_id,string description)
        {
            var ticketId =  _ticketCategoryRepository.GetTicketCategoryIdByDescriptionAndEvent(event_id,description);
           return ticketId;
        }





    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMS.Model.Dto;
using TicketMS.Repositories;
using Microsoft.Extensions.Logging;
using TicketMS.Models;
using AutoMapper;
using TicketMS.Models.Dto;
using TMS.Api.Exceptions;

namespace TicketMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        

        public EventController(IEventRepository eventRepository,IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
          
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var eventDto = events.Select(e =>_mapper.Map<EventDto>(e));
            return Ok(eventDto);
        }


        [HttpGet]
        public async Task<ActionResult<EventDto>> GetByEventId(int id)
        {
            try
            {var @event = await _eventRepository.GetByEventId(id);

    
       

            var eventDto=_mapper.Map<EventDto>(@event); 
            return Ok(eventDto);

            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(new {ErrorMessage=ex.Message});
            }
            
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
           var eventEntity= await _eventRepository.GetByEventId(eventPatch.EventId);
         
            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }

        [HttpDelete]
        public async Task <ActionResult> Delete(int id)
        {


            var eventEntity =await _eventRepository.GetByEventId(id);
    

            _eventRepository.Delete(eventEntity);
            return NoContent();
        }


    }
}
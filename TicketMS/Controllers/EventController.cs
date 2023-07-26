using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMS.Model.Dto;
using TicketMS.Repositories;
using Microsoft.Extensions.Logging;
using TicketMS.Models;
using AutoMapper;
using TicketMS.Models.Dto;

namespace TicketMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public EventController(IEventRepository eventRepository,IMapper mapper, ITicketCategoryRepository ticketCategoryRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = events.Select(e => new EventDto()
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.Location ?? string.Empty
            });


            return Ok(dtoEvents);
        }


        [HttpGet]
        public ActionResult<EventDto> GetByEventId(int id)
        {
            var @event = _eventRepository.GetByEventId(id);

            if (@event == null)
            {
                return NotFound();
            }

            //var dtoEvent = new EventDto()
            //{
            //    EventId = @event.EventId,
            //    EventDescription = @event.EventDescription,
            //    EventName = @event.EventName,
            //    EventType = @event.EventType?.EventTypeName ?? string.Empty,
            //    Venue = @event.Venue?.Location ?? string.Empty
            //};

            var eventDto=_mapper.Map<EventDto>(@event); 
            return Ok(eventDto);
        }

        [HttpPatch]
        public ActionResult<EventPatchDto> Patch(EventPatchDto eventPatch)
        {
           var eventEntity= _eventRepository.GetByEventId(eventPatch.EventId);
            if(eventEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(eventPatch, eventEntity);
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var eventEntity = _eventRepository.GetByEventId(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            //TicketCategoryPatch tcp = new TicketCategoryPatch();
            //tcp.EventId = 8;
            //_mapper.Map(tcp, eventEntity);
            //_ticketCategoryRepository.Update(eventEntity);
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }


    }
}
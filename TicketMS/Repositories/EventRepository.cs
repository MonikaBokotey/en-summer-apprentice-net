using TicketMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketMS.Repositories
{
    public class EventRepository:IEventRepository

    {
        private readonly PracticaContext _dbContext;

        public EventRepository()
        {
            _dbContext = new PracticaContext();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            var ticketC = _dbContext.TicketCategories.Where(oi => oi.EventId == @event.EventId).ToList();

            // Set the foreign key column to NULL for the related rows
            foreach (var tickets in ticketC)
            {
                tickets.EventId = null;
            }

            _dbContext.SaveChanges();

            //  _dbContext.Remove(@event);
            _dbContext.Entry(@event).State = EntityState.Deleted;
            _dbContext.SaveChanges();


           
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;

            return events;
        }

        public async Task<Event> GetByEventId(int id)
        {
            var @event =await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            return @event;
        }

        public void Update(Event @event)
        {
           _dbContext.Entry(@event).State= EntityState.Modified;
          _dbContext.SaveChanges();

        }
    }
}

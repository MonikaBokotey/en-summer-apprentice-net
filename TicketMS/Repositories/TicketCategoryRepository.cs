using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketMS.Models;

namespace TicketMS.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {

        private readonly PracticaContext _dbContext;

        public TicketCategoryRepository()
        {
            _dbContext = new PracticaContext();
        }

        public TicketCategory GetByEventId(int id)
        {
            var ticketCategory = _dbContext.TicketCategories.Where(e => e.EventId == id).FirstOrDefault();

            return ticketCategory;
        }

        public int GetTicketIdByTicketCategoryId(int id)
        {
            var ticketCategory = _dbContext.TicketCategories
                                          .Where(t => t.TicketCategoryId == id)
                                          .FirstOrDefault();

            if (ticketCategory != null)
            {
                return ticketCategory.Price;
            }

         
            return -1;
        }

        public int GetTicketCategoryIdByDescriptionAndEvent(int event_id, string description)
        {
            var ticketCategory = _dbContext.TicketCategories
                                          .Where(t => t.EventId == event_id && t.Description == description)
                                          .FirstOrDefault();

            if (ticketCategory != null)
            {
                return ticketCategory.TicketCategoryId;
            }

            return -1;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(GetByEventId(@event.EventId)).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}

using TicketMS.Models;

namespace TicketMS.Repositories
{
    public interface ITicketCategoryRepository
    {
        TicketCategory GetByEventId(int id);

        int GetTicketIdByTicketCategoryId(int id);

        public int GetTicketCategoryIdByDescriptionAndEvent(int event_id, string description);

        void Update(Event @event);


    }
}

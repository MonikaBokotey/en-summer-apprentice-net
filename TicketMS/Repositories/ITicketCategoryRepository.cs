using TicketMS.Models;

namespace TicketMS.Repositories
{
    public interface ITicketCategoryRepository
    {
        TicketCategory GetByEventId(int id);

        void Update(Event @event);


    }
}

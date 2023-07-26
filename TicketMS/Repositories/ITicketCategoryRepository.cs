using TicketMS.Models;

namespace TicketMS.Repositories
{
    public interface ITicketCategoryRepository
    {
        TicketCategory GetByEventId(int id);

        int GetTicketIdByTicketCategoryId(int id);

        void Update(Event @event);


    }
}

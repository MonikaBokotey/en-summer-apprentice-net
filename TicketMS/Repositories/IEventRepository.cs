using TicketMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketMS.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Event GetByEventId(int id);

        int Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
    }
}

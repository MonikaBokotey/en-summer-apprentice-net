using TicketMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketMS.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order GetByOrderId(int id);

        int Add(Order @order);

        void Update(Order @order);

        int Delete(int id);
    }
}

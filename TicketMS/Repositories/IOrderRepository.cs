using TicketMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketMS.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

       Task<Order> GetByOrderId(int id);

        int Add(Order @order);

        void Update(Order @order);

        void Delete(Order @order);
    }
}

using TicketMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TicketMS.Repositories



{
    public class OrderRepository : IOrderRepository
    {
    private readonly PracticaContext _dbContext;

    public OrderRepository()
    {
        _dbContext = new PracticaContext();
    }

    public int Add(Order @order)
    {
        throw new NotImplementedException();
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = _dbContext.Orders;

        return orders;
    }

    public Order GetByOrderId(int id)
    {
        var @order = _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefault();

        return @order;
    }

    public void Update(Order @order)
    {
        throw new NotImplementedException();
    }
}
}

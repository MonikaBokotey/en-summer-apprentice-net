using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketMS.Model.Dto;
using TicketMS.Models.Dto;
using TicketMS.Repositories;

namespace TicketMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                NumberOfTickets = o.NumberOfTickets,
                OrderedAt = o.OrderedAt,
                Customer = o.Customer?.CustomerName ?? string.Empty,
                TicketCategory = o.TicketCategory?.Description?? string.Empty
            });


            return Ok(dtoOrders);
        }


        [HttpGet]
        public ActionResult<OrderDto> GetByOrderId(int id)
        {
            var @orders = _orderRepository.GetByOrderId(id);

            if (@orders == null)
            {
                return NotFound();
            }

            var dtoOrder = new OrderDto()
            {
                OrderId = @orders.OrderId,
                NumberOfTickets = @orders.NumberOfTickets,
                OrderedAt = @orders.OrderedAt,
                Customer = @orders.Customer?.CustomerName ?? string.Empty,
                TicketCategory = @orders.TicketCategory?.Description ?? string.Empty
            };  

            return Ok(dtoOrder);
        }
    }
}

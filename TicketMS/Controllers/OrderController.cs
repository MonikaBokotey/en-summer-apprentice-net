using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicketMS.Model.Dto;
using TicketMS.Models;
using TicketMS.Models.Dto;
using TicketMS.Repositories;

namespace TicketMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository,IMapper mapper ,ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            //var dtoOrders = orders.Select(o => new OrderDto()
            //{
            //    OrderId = o.OrderId,
            //    NumberOfTickets = o.NumberOfTickets,
            //    OrderedAt = o.OrderedAt,
            //    Customer = o.Customer?.CustomerName ?? string.Empty,
            //    TicketCategory = o.TicketCategory?.Description?? string.Empty
            //});

            var orderDto = orders.Select(o => _mapper.Map<OrderDto>(o));
            return Ok(orderDto);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetByOrderId(int id)
        {
            var @orders =await _orderRepository.GetByOrderId(id);

            if (@orders == null)
            {
                return NotFound();
            }

            //var dtoOrder = new OrderDto()
            //{
            //    OrderId = @orders.OrderId,
            //    NumberOfTickets = @orders.NumberOfTickets,
            //    OrderedAt = @orders.OrderedAt,
            //    Customer = @orders.Customer?.CustomerName ?? string.Empty,
            //    TicketCategory = @orders.TicketCategory?.Description ?? string.Empty
            //};  


            var orderDto = _mapper.Map<OrderDto>(@orders);
            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity =await _orderRepository.GetByOrderId(orderPatch.OrderId);
            

            if (orderEntity == null)
            {
                return NotFound();
            }

            if (orderPatch.NumberOfTickets.HasValue) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            if (orderPatch.TicketCategoryId!=0) orderEntity.TicketCategoryId = orderPatch.TicketCategoryId;


            var priceOfTicket = _ticketCategoryRepository.GetTicketIdByTicketCategoryId(orderPatch.TicketCategoryId);

            if (orderEntity.TotalPrice != 0) orderEntity.TotalPrice = orderPatch.NumberOfTickets*priceOfTicket;
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetByOrderId(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}

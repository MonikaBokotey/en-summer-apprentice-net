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

     
            var orderDto = orders.Select(o => _mapper.Map<OrderDto>(o));
            return Ok(orderDto);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetByOrderId(int id)
        {
            var @orders =await _orderRepository.GetByOrderId(id);
            var orderDto = _mapper.Map<OrderDto>(@orders);
            return Ok(orderDto);
        }


        [HttpGet]
        public string GetEventNameByOrderId(int event_id)
        {
            var  eventName= _orderRepository.GetEventNameByOrderId(event_id);
            return eventName;
        }


        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity =await _orderRepository.GetByOrderId(orderPatch.OrderId);

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
        
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }
    }
}

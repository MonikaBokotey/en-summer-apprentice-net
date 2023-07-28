using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMS.Controllers;
using TicketMS.Model.Dto;
using TicketMS.Models;
using TicketMS.Models.Dto;
using TicketMS.Repositories;
using TMS.Api.Exceptions;

namespace TicketMSTest
{
    [TestClass]
    internal class OrderTest
    {
        Mock<IOrderRepository> _orderRepositoryMock;

        Mock<IMapper> _mapperMoq;
        Mock<ITicketCategoryRepository> _ticketCategoryMoq;
        List<Order> _moqList;
        List<OrderDto> _dtoMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _ticketCategoryMoq = new Mock<ITicketCategoryRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Order>
            {
                new Order {OrderId = 1,
                    NumberOfTickets = 2,
                    OrderedAt = DateTime.Now,
                    TotalPrice = 1500,
                    Customer = new Customer {CustomerId = 1},
                    CustomerId = 1,
                    TicketCategory = new TicketCategory {TicketCategoryId = 1},
                    TicketCategoryId = 1
                }
            };
            _dtoMoq = new List<OrderDto>
            {
                new OrderDto
                {
                    OrderId = 1,
                     NumberOfTickets = 2,

                    OrderedAt = DateTime.Now,

                   Customer = new Customer {CustomerId = 1,CustomerName="Mock customer",Email="mock@gmail.com"}.CustomerName,

                    TicketCategoryId = new TicketCategory {TicketCategoryId = 1,Description="Mock",Price=1500,EventId=1}.TicketCategoryId
                }
            };
        }

        [TestMethod]
        public async Task GetAllOrdersReturnListOfOrders()
        {
            //Arrange

            IReadOnlyList<Order> moqOrders = _moqList;
            Task<IReadOnlyList<Order>> moqTask = Task.Run(() => moqOrders);
            _orderRepositoryMock.Setup(moq => moq.GetAll()).Returns(_moqList);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDto>>(It.IsAny<Order>())).Returns(_dtoMoq);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object, _ticketCategoryMoq.Object);

            //Act
            var orders = controller.GetAll();
            var orderResult = orders.Result as OkObjectResult;
            var orderDtoList = orderResult.Value as IEnumerable<OrderDto>;

            //Assert

            Assert.AreEqual(_moqList.Count, orderDtoList.Count());
        }

        [TestMethod]
        public async Task GetOrderByIdReturnNotFoundWhenNoRecordFound()
        {
            //Arrange
            int eventToFind = 11;
            _orderRepositoryMock.Setup(moq => moq.GetByOrderId(eventToFind)).ThrowsAsync(new EntityNotFoundException(eventToFind, nameof(Event)));

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object,_ticketCategoryMoq.Object);
            //Act

            var result = await controller.GetByOrderId(eventToFind);
            var orderResult = result.Result as NotFoundObjectResult;


            //Assert

            Assert.IsTrue(orderResult.StatusCode == 404);
        }

        [TestMethod]
        public async Task GetOrderByIdReturnFirstRecord()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetByOrderId(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<OrderDto>(It.IsAny<Event>())).Returns(_dtoMoq.First());
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object,_ticketCategoryMoq.Object);
            //Act

            var result = await controller.GetByOrderId(1);
            var orderResult = result.Result as OkObjectResult;
            var orderCount = orderResult.Value as OrderDto;

            //Assert

            
            Assert.AreEqual(1, orderCount.OrderId);
        }

    }
}

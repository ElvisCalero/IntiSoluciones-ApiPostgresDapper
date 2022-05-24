using ApiPostgresDapper.Domain.DTOs;
using ApiPostgresDapper.Domain.Entities;
using ApirPostgresDapper.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgresDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);
            var orderDetail = await _orderDetailRepository.GetById(order.Id);
            OrderDTO orderDTO = new()
            {
                Id = order.Id,
                Customer_Id = order.Customer_Id,
                Order_Date = order.Order_Date,
                orderDetails = orderDetail.ToList()
            };            
            return Ok(orderDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderDTO entity)
        {            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Order newOrder = new()
            {
                Customer_Id = entity.Customer_Id,
                Order_Date = entity.Order_Date
            };
            var orderId = await _orderRepository.Add(newOrder);
            foreach (var item in entity.orderDetails)
            {
                OrderDetail newDetail = new()
                {
                    Order_Id = orderId,
                    Product_Id = item.Product_Id,
                    Quantity = item.Quantity,
                    Unit_Price = item.Unit_Price,
                    Total_Price = item.Total_Price
                };
                await _orderDetailRepository.Add(newDetail);
            }
            return Created("Created", orderId);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDTO entity)
        {
            if (id != entity.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Order newOrder = new()
            {
                Customer_Id = entity.Customer_Id,
                Order_Date = entity.Order_Date
            };
            await _orderRepository.Update(newOrder);
            
            foreach (var item in entity.orderDetails)
            {
                OrderDetail newDetail = new()
                {
                    Order_Id = item.Order_Id,
                    Product_Id = item.Product_Id,
                    Quantity = item.Quantity,
                    Unit_Price = item.Unit_Price,
                    Total_Price = item.Total_Price
                };
                await _orderDetailRepository.Update(newDetail);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderRepository.Delete(id);
            return NoContent();
        }
    }
}

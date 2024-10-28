using EFMBL.IOrderServise;
using EFMBL.OrderDto;
using Microsoft.AspNetCore.Mvc;

namespace EFMBL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /*
        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.AddOrderAsync(order);
            return result ? Ok("Order added successfully.") : StatusCode(500, "Error adding order.");
        }
        */
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("sort-between")]
        public async Task<IActionResult> SortOrdersBetween([FromQuery] string districtName, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var sortedOrders = await _orderService.SortOrdersBetweenAsync(districtName, fromDate, toDate);
            return Ok(sortedOrders);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortOrders([FromQuery] string districtName)
        {
            var sortedOrders = await _orderService.SortOrdersAsync(districtName);
            return Ok(sortedOrders);
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetAllSortedOrders()
        {
            var sortedOrders = await _orderService.GetAllSortedOrdersAsync();
            return Ok(sortedOrders);
        }
    }
}

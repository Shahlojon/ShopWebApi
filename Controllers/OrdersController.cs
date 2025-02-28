using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dto.OrderDtos;
using ShopApi.Enums;
using ShopApi.Interfaces.Services;
using ShopApi.Responses;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IOrderService orderService): ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(new ApiResponse<IEnumerable<OrderResponseDto>>(await orderService.GetAllOrdersAsync()));
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            return Ok(new ApiResponse<OrderResponseDto>(await orderService.GetOrderByIdAsync(id)));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto request)
        {
            return Ok(new ApiResponse<int>(await orderService.CreateOrderAsync(request)));
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return Ok(new ApiResponse<bool>(await orderService.DeleteOrderAsync(id)));
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(int id, Status status)
        {
            return Ok(new ApiResponse<bool>(await orderService.UpdateOrderStatusAsync(id,status)));
        }
    }
}

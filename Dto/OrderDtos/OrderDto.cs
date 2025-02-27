using ShopApi.Dto.UserDtos;
using ShopApi.Entites;
using ShopApi.Enums;

namespace ShopApi.Dto.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; } // PK
        public int UserId { get; set; } // FK -> Users
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Pending; // "Pending", "Shipped", "Completed", "Canceled"

        public UserDto User { get; set; }
    }
}

using ShopApi.Enums;

namespace ShopApi.Entites;

public class User
{
    public int Id { get; set; } // PK
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public UserProfile UserProfile { get; set; } 
}
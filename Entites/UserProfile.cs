namespace ShopApi.Entites;

public class UserProfile:DataFile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
}
namespace ShopApi.Dto.UserDtos;

public class UserProfileResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
    public string Extension { get; set; }
}
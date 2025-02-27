namespace ShopApi.Dto.UserDtos.Requests;

public record struct UserProfileRequestDto(int UserId, IFormFile File);
using ShopApi.Dto.UserDtos.Requests;
using ShopApi.Entites;

namespace ShopApi.Interfaces.Services;

public interface IUserProfileService
{
    Task<UserProfile> CreateAsync(UserProfileRequestDto requestDto);
    Task<UserProfile> UpdateAsync(UserProfileRequestDto requestDto);
    Task<bool> DeleteAsync(int userId);
    Task<User> GetProfileAsync(int userId);
}
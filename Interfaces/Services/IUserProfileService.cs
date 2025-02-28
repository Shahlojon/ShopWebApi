using ShopApi.Dto.UserDtos;
using ShopApi.Dto.UserDtos.Requests;
using ShopApi.Entites;

namespace ShopApi.Interfaces.Services;

public interface IUserProfileService
{
    Task<UserProfileResponseDto> CreateAsync(UserProfileRequestDto requestDto);
    Task<UserProfileResponseDto> UpdateAsync(UserProfileRequestDto requestDto);
    Task<bool> DeleteAsync(int userId);
    Task<UserResponseDto> GetProfileAsync(int userId);
}
using System.ComponentModel.DataAnnotations;
using ShopApi.Dto;
using ShopApi.Dto.UserDtos.Requests;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class UserProfileService(IUserProfileRepository repository, 
    IUserRepository userRepository, 
    IFileService fileService):IUserProfileService
{
    public async Task<UserProfile> CreateAsync(UserProfileRequestDto requestDto)
    {
        if (await repository.ExistAsync(requestDto.UserId))
            throw new ValidationException(
                "Вы не можете добавить данные в профиль уже есть такие данные попробуйте обновить данные");

        var fileDto = await fileService.SaveAsync(requestDto.File);
        UserProfile userProfile = new()
        {
            Name = fileDto.Name,
            Url = fileDto.Url,
            Extension = fileDto.Extension,
            Size = fileDto.Size,
            UserId = requestDto.UserId
        };
        await repository.AddAsync(userProfile);
        return userProfile;
    }

    public async Task<UserProfile> UpdateAsync(UserProfileRequestDto requestDto)
    {
        UserProfile userProfile = await repository.GetByUserIdAsync(requestDto.UserId);
        await fileService.RemoveAsync(userProfile.Url);
        FileDto fileDto  = await fileService.SaveAsync(requestDto.File);
        userProfile.Url = fileDto.Url;
        userProfile.Name = fileDto.Name;
        userProfile.Extension = fileDto.Extension;
        userProfile.Size = fileDto.Size;
        await repository.UpdateAsync(userProfile);
        return userProfile;

    }

    public async Task<bool> DeleteAsync(int userId)
    {
        UserProfile userProfile = await repository.GetByUserIdAsync(userId);
        if (userProfile is null) return false;
        await fileService.RemoveAsync(userProfile.Url);
        return true;
    }
    public async Task<User> GetProfileAsync(int userId)
    {
        return await userRepository.GetByIdAsync(userId);
    }
}
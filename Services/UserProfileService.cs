using System.ComponentModel.DataAnnotations;
using ShopApi.Dto;
using ShopApi.Dto.UserDtos;
using ShopApi.Dto.UserDtos.Requests;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class UserProfileService(IUserProfileRepository repository, 
    IUserRepository userRepository, 
    IFileService fileService):IUserProfileService
{
    public async Task<UserProfileResponseDto> CreateAsync(UserProfileRequestDto requestDto)
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
            return new UserProfileResponseDto()
            {
                 Id = userProfile.Id,
                 UserId = userProfile.UserId,
                 Url = userProfile.Url,
                 Size = userProfile.Size,
                 Name = userProfile.Name
            };
    }

    public async Task<UserProfileResponseDto> UpdateAsync(UserProfileRequestDto requestDto)
    {
        UserProfile userProfile = await repository.GetByUserIdAsync(requestDto.UserId);
        await fileService.RemoveAsync(userProfile.Url);
        FileDto fileDto  = await fileService.SaveAsync(requestDto.File);
        userProfile.Url = fileDto.Url;
        userProfile.Name = fileDto.Name;
        userProfile.Extension = fileDto.Extension;
        userProfile.Size = fileDto.Size;
        await repository.UpdateAsync(userProfile);
        return new UserProfileResponseDto()
        {
            Id = userProfile.Id,
            UserId = userProfile.UserId,
            Url = userProfile.Url,
            Size = userProfile.Size,
            Name = userProfile.Name
        };

    }

    public async Task<bool> DeleteAsync(int userId)
    {
        UserProfile userProfile = await repository.GetByUserIdAsync(userId);
        if (userProfile is null) return false;
        await fileService.RemoveAsync(userProfile.Url);
        return true;
    }
    public async Task<UserResponseDto> GetProfileAsync(int userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        return new UserResponseDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role  = user.Role,
            UserProfile = user.UserProfile!=null? new UserProfileResponseDto()
            {
                Id = user.UserProfile.Id,
                UserId = user.UserProfile.UserId,
                Url = user.UserProfile.Url,
                Size = user.UserProfile.Size,
                Name = user.UserProfile.Name
            }: new UserProfileResponseDto()
        };
    }
}
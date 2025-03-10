using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dto.UserDtos;
using ShopApi.Dto.UserDtos.Requests;
using ShopApi.Entites;
using ShopApi.Interfaces.Services;
using ShopApi.Responses;

namespace ShopApi.Controllers
{
    [Route("api/users/profile")]
    [ApiController]
    [Authorize]
    public class UserProfilesController(IUserProfileService profileService) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(new ApiResponse<UserResponseDto>(await profileService.GetProfileAsync(UserId)));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(IFormFile file)
        {
            UserProfileRequestDto requestDto = new(UserId, file);
            return Ok(new ApiResponse<UserProfileResponseDto>(await profileService.CreateAsync(requestDto)));
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(IFormFile file)
        {
            UserProfileRequestDto requestDto = new(UserId, file);
            return Ok(new ApiResponse<UserProfileResponseDto>(await profileService.UpdateAsync(requestDto)));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return Ok(new ApiResponse<bool>(await profileService.DeleteAsync(UserId)));
        }
    }
}

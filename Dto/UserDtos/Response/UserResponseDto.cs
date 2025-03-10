﻿using ShopApi.Enums;

namespace ShopApi.Dto.UserDtos;

public class UserResponseDto
{
    public int Id {  get; set; }
    public string FirstName {  get; set; }
    public string LastName {  get; set; }
    public string Email {  get; set; }
    public Role Role { get; set; }
    public UserProfileResponseDto? UserProfile { get; set; }
}

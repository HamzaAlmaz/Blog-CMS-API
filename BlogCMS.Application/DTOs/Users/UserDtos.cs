using System;

namespace BlogCMS.Application.DTOs.Users;

public record CreateUserDto(string Username, string Email, string Password, Guid RoleId);
public record UpdateUserDto(Guid Id, string Username, string Email, Guid RoleId);
public record UserResponseDto(Guid Id, string Username, string Email, Guid RoleId);

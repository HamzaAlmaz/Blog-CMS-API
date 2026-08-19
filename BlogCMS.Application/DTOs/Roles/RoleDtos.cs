using System;

namespace BlogCMS.Application.DTOs.Roles;

public record CreateRoleDto(string Name);
public record UpdateRoleDto(Guid Id, string Name);
public record RoleResponseDto(Guid Id, string Name);

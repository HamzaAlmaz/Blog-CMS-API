using System;

namespace BlogCMS.Application.DTOs.Categories;

public record CreateCategoryDto(string Name, string Description);
public record UpdateCategoryDto(Guid Id, string Name, string Description);
public record CategoryResponseDto(Guid Id, string Name, string Description);

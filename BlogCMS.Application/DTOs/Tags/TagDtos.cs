using System;

namespace BlogCMS.Application.DTOs.Tags;

public record CreateTagDto(string Name);
public record UpdateTagDto(Guid Id, string Name);
public record TagResponseDto(Guid Id, string Name);

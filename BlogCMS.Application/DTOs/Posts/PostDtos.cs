using System;
using System.Collections.Generic;

namespace BlogCMS.Application.DTOs.Posts;

public record CreatePostDto(string Title, string Content, Guid CategoryId, List<Guid> TagIds);
public record UpdatePostDto(Guid Id, string Title, string Content, bool IsPublished, Guid CategoryId, List<Guid> TagIds);
public record PostResponseDto(Guid Id, string Title, string Content, bool IsPublished, Guid CategoryId, Guid UserId, DateTime CreatedDate);

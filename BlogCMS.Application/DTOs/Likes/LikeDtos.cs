using System;

namespace BlogCMS.Application.DTOs.Likes;

public record CreateLikeDto(Guid PostId, Guid UserId);
public record LikeResponseDto(Guid Id, Guid PostId, Guid UserId);

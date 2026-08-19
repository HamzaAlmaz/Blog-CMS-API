using System;

namespace BlogCMS.Application.DTOs.Comments;

public record CreateCommentDto(string Content, Guid PostId, Guid UserId);
public record UpdateCommentDto(Guid Id, string Content);
public record CommentResponseDto(Guid Id, string Content, Guid PostId, Guid UserId, DateTime CreatedDate);

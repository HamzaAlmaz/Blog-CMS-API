using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Comments;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Comments;

public record CreateCommentCommand(CreateCommentDto Dto) : IRequest<Guid>;
public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly ICommentRepository _repository;
    public CreateCommentCommandHandler(ICommentRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Comment 
        { 
            Content = request.Dto.Content, 
            PostId = request.Dto.PostId,
            UserId = request.Dto.UserId,
            CreatedDate = DateTime.UtcNow 
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdateCommentCommand(UpdateCommentDto Dto) : IRequest<bool>;
public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, bool>
{
    private readonly ICommentRepository _repository;
    public UpdateCommentCommandHandler(ICommentRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;
        entity.Content = request.Dto.Content;
        entity.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeleteCommentCommand(Guid Id) : IRequest<bool>;
public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly ICommentRepository _repository;
    public DeleteCommentCommandHandler(ICommentRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetCommentByIdQuery(Guid Id) : IRequest<CommentResponseDto>;
public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentResponseDto>
{
    private readonly ICommentRepository _repository;
    public GetCommentByIdQueryHandler(ICommentRepository repository) => _repository = repository;

    public async Task<CommentResponseDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new CommentResponseDto(entity.Id, entity.Content, entity.PostId, entity.UserId, entity.CreatedDate);
    }
}

public record GetAllCommentsQuery() : IRequest<IEnumerable<CommentResponseDto>>;
public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentResponseDto>>
{
    private readonly ICommentRepository _repository;
    public GetAllCommentsQueryHandler(ICommentRepository repository) => _repository = repository;

    public async Task<IEnumerable<CommentResponseDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new CommentResponseDto(e.Id, e.Content, e.PostId, e.UserId, e.CreatedDate));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Posts;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Posts;

public record CreatePostCommand(CreatePostDto Dto) : IRequest<Guid>;
public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IPostRepository _repository;
    public CreatePostCommandHandler(IPostRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = new Post 
        { 
            Title = request.Dto.Title, 
            Content = request.Dto.Content, 
            CategoryId = request.Dto.CategoryId,
            CreatedDate = DateTime.UtcNow 
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdatePostCommand(UpdatePostDto Dto) : IRequest<bool>;
public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IPostRepository _repository;
    public UpdatePostCommandHandler(IPostRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;

        entity.Title = request.Dto.Title;
        entity.Content = request.Dto.Content;
        entity.IsPublished = request.Dto.IsPublished;
        entity.CategoryId = request.Dto.CategoryId;
        entity.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeletePostCommand(Guid Id) : IRequest<bool>;
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _repository;
    public DeletePostCommandHandler(IPostRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;

        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetPostByIdQuery(Guid Id) : IRequest<PostResponseDto>;
public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostResponseDto>
{
    private readonly IPostRepository _repository;
    public GetPostByIdQueryHandler(IPostRepository repository) => _repository = repository;

    public async Task<PostResponseDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new PostResponseDto(entity.Id, entity.Title, entity.Content, entity.IsPublished, entity.CategoryId, entity.UserId, entity.CreatedDate);
    }
}

public record GetAllPostsQuery() : IRequest<IEnumerable<PostResponseDto>>;
public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostResponseDto>>
{
    private readonly IPostRepository _repository;
    public GetAllPostsQueryHandler(IPostRepository repository) => _repository = repository;

    public async Task<IEnumerable<PostResponseDto>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new PostResponseDto(e.Id, e.Title, e.Content, e.IsPublished, e.CategoryId, e.UserId, e.CreatedDate));
    }
}

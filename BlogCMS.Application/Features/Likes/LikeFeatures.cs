using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Likes;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Likes;

public record CreateLikeCommand(CreateLikeDto Dto) : IRequest<Guid>;
public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Guid>
{
    private readonly ILikeRepository _repository;
    public CreateLikeCommandHandler(ILikeRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Like 
        { 
            PostId = request.Dto.PostId,
            UserId = request.Dto.UserId,
            CreatedDate = DateTime.UtcNow 
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record DeleteLikeCommand(Guid Id) : IRequest<bool>;
public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, bool>
{
    private readonly ILikeRepository _repository;
    public DeleteLikeCommandHandler(ILikeRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetAllLikesQuery() : IRequest<IEnumerable<LikeResponseDto>>;
public class GetAllLikesQueryHandler : IRequestHandler<GetAllLikesQuery, IEnumerable<LikeResponseDto>>
{
    private readonly ILikeRepository _repository;
    public GetAllLikesQueryHandler(ILikeRepository repository) => _repository = repository;

    public async Task<IEnumerable<LikeResponseDto>> Handle(GetAllLikesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new LikeResponseDto(e.Id, e.PostId, e.UserId));
    }
}

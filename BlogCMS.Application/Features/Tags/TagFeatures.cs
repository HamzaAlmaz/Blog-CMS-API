using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Tags;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Tags;

public record CreateTagCommand(CreateTagDto Dto) : IRequest<Guid>;
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly ITagRepository _repository;
    public CreateTagCommandHandler(ITagRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new Tag { Name = request.Dto.Name, CreatedDate = DateTime.UtcNow };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdateTagCommand(UpdateTagDto Dto) : IRequest<bool>;
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, bool>
{
    private readonly ITagRepository _repository;
    public UpdateTagCommandHandler(ITagRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;
        entity.Name = request.Dto.Name;
        entity.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeleteTagCommand(Guid Id) : IRequest<bool>;
public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
{
    private readonly ITagRepository _repository;
    public DeleteTagCommandHandler(ITagRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetTagByIdQuery(Guid Id) : IRequest<TagResponseDto>;
public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagResponseDto>
{
    private readonly ITagRepository _repository;
    public GetTagByIdQueryHandler(ITagRepository repository) => _repository = repository;

    public async Task<TagResponseDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new TagResponseDto(entity.Id, entity.Name);
    }
}

public record GetAllTagsQuery() : IRequest<IEnumerable<TagResponseDto>>;
public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<TagResponseDto>>
{
    private readonly ITagRepository _repository;
    public GetAllTagsQueryHandler(ITagRepository repository) => _repository = repository;

    public async Task<IEnumerable<TagResponseDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new TagResponseDto(e.Id, e.Name));
    }
}

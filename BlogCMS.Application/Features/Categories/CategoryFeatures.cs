using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Categories;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Categories;

public record CreateCategoryCommand(CreateCategoryDto Dto) : IRequest<Guid>;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _repository;
    public CreateCategoryCommandHandler(ICategoryRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category { Name = request.Dto.Name, Description = request.Dto.Description, CreatedDate = DateTime.UtcNow };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdateCategoryCommand(UpdateCategoryDto Dto) : IRequest<bool>;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
{
    private readonly ICategoryRepository _repository;
    public UpdateCategoryCommandHandler(ICategoryRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;
        entity.Name = request.Dto.Name;
        entity.Description = request.Dto.Description;
        entity.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoryRepository _repository;
    public DeleteCategoryCommandHandler(ICategoryRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponseDto>;
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponseDto>
{
    private readonly ICategoryRepository _repository;
    public GetCategoryByIdQueryHandler(ICategoryRepository repository) => _repository = repository;

    public async Task<CategoryResponseDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new CategoryResponseDto(entity.Id, entity.Name, entity.Description);
    }
}

public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryResponseDto>>;
public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResponseDto>>
{
    private readonly ICategoryRepository _repository;
    public GetAllCategoriesQueryHandler(ICategoryRepository repository) => _repository = repository;

    public async Task<IEnumerable<CategoryResponseDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new CategoryResponseDto(e.Id, e.Name, e.Description));
    }
}

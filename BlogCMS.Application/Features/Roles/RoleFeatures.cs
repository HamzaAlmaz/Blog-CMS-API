using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Roles;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Roles;

public record CreateRoleCommand(CreateRoleDto Dto) : IRequest<Guid>;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IRoleRepository _repository;
    public CreateRoleCommandHandler(IRoleRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = new Role { Name = request.Dto.Name, CreatedDate = DateTime.UtcNow };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdateRoleCommand(UpdateRoleDto Dto) : IRequest<bool>;
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
{
    private readonly IRoleRepository _repository;
    public UpdateRoleCommandHandler(IRoleRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;
        entity.Name = request.Dto.Name;
        entity.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _repository;
    public DeleteRoleCommandHandler(IRoleRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetRoleByIdQuery(Guid Id) : IRequest<RoleResponseDto>;
public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleResponseDto>
{
    private readonly IRoleRepository _repository;
    public GetRoleByIdQueryHandler(IRoleRepository repository) => _repository = repository;

    public async Task<RoleResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new RoleResponseDto(entity.Id, entity.Name);
    }
}

public record GetAllRolesQuery() : IRequest<IEnumerable<RoleResponseDto>>;
public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponseDto>>
{
    private readonly IRoleRepository _repository;
    public GetAllRolesQueryHandler(IRoleRepository repository) => _repository = repository;

    public async Task<IEnumerable<RoleResponseDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new RoleResponseDto(e.Id, e.Name));
    }
}

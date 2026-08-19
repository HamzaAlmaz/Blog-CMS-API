using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogCMS.Application.DTOs.Users;
using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;

namespace BlogCMS.Application.Features.Users;

public record CreateUserCommand(CreateUserDto Dto) : IRequest<Guid>;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;
    public CreateUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User 
        { 
            Username = request.Dto.Username, 
            Email = request.Dto.Email, 
            PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Dto.Password)),
            RoleId = request.Dto.RoleId,
            CreatedDate = DateTime.UtcNow 
        };
        await _repository.AddAsync(entity);
        return entity.Id;
    }
}

public record UpdateUserCommand(UpdateUserDto Dto) : IRequest<bool>;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _repository;
    public UpdateUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Dto.Id);
        if (entity == null) return false;
        entity.Username = request.Dto.Username;
        entity.Email = request.Dto.Email;
        entity.RoleId = request.Dto.RoleId;
        entity.UpdatedDate = DateTime.UtcNow;
        await _repository.UpdateAsync(entity);
        return true;
    }
}

public record DeleteUserCommand(Guid Id) : IRequest<bool>;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;
    public DeleteUserCommandHandler(IUserRepository repository) => _repository = repository;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }
}

public record GetUserByIdQuery(Guid Id) : IRequest<UserResponseDto>;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto>
{
    private readonly IUserRepository _repository;
    public GetUserByIdQueryHandler(IUserRepository repository) => _repository = repository;

    public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        return entity == null ? null : new UserResponseDto(entity.Id, entity.Username, entity.Email, entity.RoleId);
    }
}

public record GetAllUsersQuery() : IRequest<IEnumerable<UserResponseDto>>;
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseDto>>
{
    private readonly IUserRepository _repository;
    public GetAllUsersQueryHandler(IUserRepository repository) => _repository = repository;

    public async Task<IEnumerable<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => new UserResponseDto(e.Id, e.Username, e.Email, e.RoleId));
    }
}

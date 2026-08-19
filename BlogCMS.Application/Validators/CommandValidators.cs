using FluentValidation;
using BlogCMS.Application.Features.Posts;
using BlogCMS.Application.Features.Categories;
using BlogCMS.Application.Features.Users;
using BlogCMS.Application.Features.Roles;
using BlogCMS.Application.Features.Tags;
using BlogCMS.Application.Features.Comments;
using BlogCMS.Application.Features.Likes;

namespace BlogCMS.Application.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(v => v.Dto.Title).NotEmpty().MaximumLength(200);
        RuleFor(v => v.Dto.Content).NotEmpty();
        RuleFor(v => v.Dto.CategoryId).NotEmpty();
    }
}

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(v => v.Dto.Id).NotEmpty();
        RuleFor(v => v.Dto.Title).NotEmpty().MaximumLength(200);
        RuleFor(v => v.Dto.Content).NotEmpty();
        RuleFor(v => v.Dto.CategoryId).NotEmpty();
    }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Dto.Name).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Dto.Description).MaximumLength(500);
    }
}

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Dto.Id).NotEmpty();
        RuleFor(v => v.Dto.Name).NotEmpty().MaximumLength(100);
        RuleFor(v => v.Dto.Description).MaximumLength(500);
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Dto.Username).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Dto.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(v => v.Dto.Password).NotEmpty().MinimumLength(6);
        RuleFor(v => v.Dto.RoleId).NotEmpty();
    }
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.Dto.Id).NotEmpty();
        RuleFor(v => v.Dto.Username).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Dto.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(v => v.Dto.RoleId).NotEmpty();
    }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(v => v.Dto.Name).NotEmpty().MaximumLength(50);
    }
}

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(v => v.Dto.Name).NotEmpty().MaximumLength(50);
    }
}

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(v => v.Dto.Content).NotEmpty().MaximumLength(1000);
        RuleFor(v => v.Dto.PostId).NotEmpty();
        RuleFor(v => v.Dto.UserId).NotEmpty();
    }
}

public class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
{
    public CreateLikeCommandValidator()
    {
        RuleFor(v => v.Dto.PostId).NotEmpty();
        RuleFor(v => v.Dto.UserId).NotEmpty();
    }
}

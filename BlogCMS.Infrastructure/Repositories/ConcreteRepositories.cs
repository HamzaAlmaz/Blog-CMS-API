using BlogCMS.Application.Interfaces.Repositories;
using BlogCMS.Domain.Entities;
using BlogCMS.Infrastructure.Data;

namespace BlogCMS.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(BlogDbContext context) : base(context) { }
}

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(BlogDbContext context) : base(context) { }
}

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    public PostRepository(BlogDbContext context) : base(context) { }
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(BlogDbContext context) : base(context) { }
}

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(BlogDbContext context) : base(context) { }
}

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(BlogDbContext context) : base(context) { }
}

public class LikeRepository : GenericRepository<Like>, ILikeRepository
{
    public LikeRepository(BlogDbContext context) : base(context) { }
}

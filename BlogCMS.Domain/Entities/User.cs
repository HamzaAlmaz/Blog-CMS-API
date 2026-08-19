using System;
using System.Collections.Generic;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        Posts = new HashSet<Post>();
        Comments = new HashSet<Comment>();
        Likes = new HashSet<Like>();
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
}

using System;
using System.Collections.Generic;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class Post : BaseEntity
{
    public Post()
    {
        Tags = new HashSet<Tag>();
        Comments = new HashSet<Comment>();
        Likes = new HashSet<Like>();
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsPublished { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Tag> Tags { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
}

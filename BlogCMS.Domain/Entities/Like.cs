using System;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class Like : BaseEntity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}

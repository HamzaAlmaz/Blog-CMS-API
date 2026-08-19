using System.Collections.Generic;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class Tag : BaseEntity
{
    public Tag()
    {
        Posts = new HashSet<Post>();
    }

    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}

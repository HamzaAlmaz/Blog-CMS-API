using System.Collections.Generic;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class Category : BaseEntity
{
    public Category()
    {
        Posts = new HashSet<Post>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Post> Posts { get; set; }
}

using System.Collections.Generic;
using BlogCMS.Domain.Entities.Common;

namespace BlogCMS.Domain.Entities;

public class Role : BaseEntity
{
    public Role()
    {
        Users = new HashSet<User>();
    }

    public string Name { get; set; }

    public ICollection<User> Users { get; set; }
}

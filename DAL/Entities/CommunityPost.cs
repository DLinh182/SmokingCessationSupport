using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class CommunityPost
{
    public int PostId { get; set; }

    public int? AccountId { get; set; }

    public DateOnly? CreateTime { get; set; }

    public string? Content { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

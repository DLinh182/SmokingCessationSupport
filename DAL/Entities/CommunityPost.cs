using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class CommunityPost
{
    public int PostId { get; set; }

    public int AccountId { get; set; }

    public DateOnly CreateTime { get; set; }

    public string Content { get; set; } = null!; // Content cannot be null, so we use null-forgiving operator

    public virtual Account? Account { get; set; }//post.Account.User.FullName

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

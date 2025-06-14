using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Comment
{
    public int CmtId { get; set; }

    public int AccountId { get; set; }

    public int PostId { get; set; }

    public string Comment1 { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual Account? Account { get; set; }

    public virtual CommunityPost? Post { get; set; }
}

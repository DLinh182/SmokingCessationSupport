using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Ranking
{
    public int RankingId { get; set; }

    public int? MemberId { get; set; }

    public string? Badge { get; set; }

    public int? TotalScore { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Member? Member { get; set; }
}

using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Plan
{
    public int PlanId { get; set; }

    public int? MemberId { get; set; }

    public DateOnly? QuitSmokingDate { get; set; }

    public decimal? SaveMoney { get; set; }

    public DateTime? Clock { get; set; }

    public int? CigarettesQuit { get; set; }

    public int? MaxCigarettes { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<Phase> Phases { get; set; } = new List<Phase>();

    public virtual ICollection<PlanDetail> PlanDetails { get; set; } = new List<PlanDetail>();
}

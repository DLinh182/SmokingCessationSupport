using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Phase
{
    public int PhaseId { get; set; }

    public int? PlanId { get; set; }

    public int? PhaseNumber { get; set; }

    public DateOnly? StartDatePhase { get; set; }

    public DateOnly? EndDatePhase { get; set; }

    public string? StatusPhase { get; set; }

    public virtual Plan? Plan { get; set; }
}

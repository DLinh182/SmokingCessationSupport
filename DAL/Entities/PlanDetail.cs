using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class PlanDetail
{
    public int PlanDetailId { get; set; }

    public int? PlanId { get; set; }

    public int? TodayCigarettes { get; set; }

    public int? MaxCigarettes { get; set; }

    public DateOnly? Date { get; set; }

    public bool? IsSuccess { get; set; }

    public virtual Plan? Plan { get; set; }
}

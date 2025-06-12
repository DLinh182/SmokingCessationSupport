using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? MemberId { get; set; }

    public int? PackageMembershipId { get; set; }

    public DateTime? TimeBuy { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Member? Member { get; set; }

    public virtual PackageMembership? PackageMembership { get; set; }
}

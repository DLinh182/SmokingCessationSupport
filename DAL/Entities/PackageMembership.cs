using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class PackageMembership
{
    public int PackageMembershipId { get; set; }

    public string Category { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}

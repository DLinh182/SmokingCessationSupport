using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int AccountId { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateOnly? Birthday { get; set; }

    public bool? Sex { get; set; }

    public string? Role { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Member? Member { get; set; }
}

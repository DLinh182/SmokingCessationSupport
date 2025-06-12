using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public int AccountId { get; set; }

    public int? CigarettesPerDay { get; set; }

    public string? SmokingTime { get; set; }

    public int? GoalTime { get; set; }

    public string? Reason { get; set; }

    public decimal? CostPerCigarette { get; set; }

    public string? MedicalHistory { get; set; }

    public string? MostSmokingTime { get; set; }

    public string? FeedbackContent { get; set; }

    public DateOnly? FeedbackDate { get; set; }

    public int? FeedbackRating { get; set; }

    public string? StatusProcess { get; set; }

    public virtual User? Account { get; set; }

    public virtual Plan? Plan { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();
}

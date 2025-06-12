﻿using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Platform
{
    public int PlatformId { get; set; }

    public string? News1Title { get; set; }

    public string? News1Content { get; set; }

    public string? News1Link { get; set; }

    public string? News2Title { get; set; }

    public string? News2Content { get; set; }

    public string? News2Link { get; set; }

    public string? News3Title { get; set; }

    public string? News3Content { get; set; }

    public string? News3Link { get; set; }

    public string? Message { get; set; }

    public string? About { get; set; }

    public string? Benefit { get; set; }

    public DateTime? LastUpdated { get; set; }
}

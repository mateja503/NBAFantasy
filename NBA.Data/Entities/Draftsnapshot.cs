using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Draftsnapshot
{
    public long Leagueid { get; set; }

    public string? Draftstate { get; set; }

    public string? Draftteams { get; set; }

    public DateTime Tsupdated { get; set; }

    public virtual League League { get; set; } = null!;
}

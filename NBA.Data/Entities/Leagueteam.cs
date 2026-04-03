using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Leagueteam
{
    public long Leagueteamid { get; set; }

    public long Teamid { get; set; }

    public long Leagueid { get; set; }

    public bool Approved { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}

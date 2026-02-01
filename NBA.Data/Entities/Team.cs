using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Team
{
    public long Teamid { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Tscreated { get; set; }

    public DateTime? Tsupdated { get; set; }

    public string Usercreated { get; set; } = null!;

    public string? Userupdated { get; set; }

    public virtual ICollection<Leagueteam> Leagueteams { get; set; } = new List<Leagueteam>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Ranglistteam> Ranglistteams { get; set; } = new List<Ranglistteam>();
}

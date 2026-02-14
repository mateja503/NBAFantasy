using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Team
{
    public long Teamid { get; set; }

    public string Name { get; set; } = null!;

    public int? Seed { get; set; }

    public int? Waiverpriority { get; set; }

    public double? Lastweekpoints { get; set; }

    public double? Categoryleaguepoints { get; set; }

    public bool? Islock { get; set; }

    public virtual ICollection<Leagueteam> Leagueteams { get; set; } = new List<Leagueteam>();

    public virtual ICollection<Teamplayer> Teamplayers { get; set; } = new List<Teamplayer>();

    public virtual ICollection<Userteam> Userteams { get; set; } = new List<Userteam>();
}

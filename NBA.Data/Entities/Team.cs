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

    public bool Approved { get; set; }

    public bool? Islock { get; set; }

    public long? Userid { get; set; }

    public long? Leagueid { get; set; }

    public virtual League? League { get; set; }

    public virtual ICollection<Teamplayer> Teamplayers { get; set; } = new List<Teamplayer>();

    public virtual Applicationuser? User { get; set; }
}

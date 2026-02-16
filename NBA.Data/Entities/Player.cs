using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Player
{
    public long Playerid { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Irlteamname { get; set; }

    public long? Irlteamid { get; set; }

    public long? Points { get; set; }

    public long? Assists { get; set; }

    public long? Rebounds { get; set; }

    public long? Blocks { get; set; }

    public long? Steals { get; set; }

    public long? Threepointers { get; set; }

    public long? Turnovers { get; set; }

    public double? Freethrowperc { get; set; }

    public double? Fieldgoalperc { get; set; }

    public bool? Isdrop { get; set; }

    public bool? Isfreeagent { get; set; }

    public bool? Allowdrop { get; set; }

    public bool? Islock { get; set; }

    public DateTime? Tsupdated { get; set; }

    public DateTime? Tscreated { get; set; }

    public int? Playerposition { get; set; }

    public int? Rosterrole { get; set; }

    public int? Gameready { get; set; }

    public long? Playermemontoid { get; set; }

    public virtual ICollection<Leagueplayer> Leagueplayers { get; set; } = new List<Leagueplayer>();

    public virtual Playermemento? Playermemonto { get; set; }

    public virtual ICollection<Teamplayer> Teamplayers { get; set; } = new List<Teamplayer>();
}

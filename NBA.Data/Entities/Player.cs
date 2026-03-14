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

    public decimal? Points { get; set; }

    public decimal? Assists { get; set; }

    public decimal? Rebounds { get; set; }

    public decimal? Blocks { get; set; }

    public decimal? Steals { get; set; }

    public decimal? Threepointers { get; set; }

    public decimal? Turnovers { get; set; }

    public decimal? Freethrow { get; set; }

    public decimal? Fieldgoal { get; set; }

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

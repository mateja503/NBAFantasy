using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class League
{
    public long Leagueid { get; set; }

    public string Name { get; set; } = null!;

    public long Commissioner { get; set; }

    public string Seasonyear { get; set; } = null!;

    public int? Weeksforseason { get; set; }

    public int? Transactionlimit { get; set; }

    public bool? Autostart { get; set; }

    public int? Typetransactionlimits { get; set; }

    public int? Typeleague { get; set; }

    public int? Draftstyle { get; set; }

    public long? Statsvalueid { get; set; }

    public virtual ICollection<Leagueplayer> Leagueplayers { get; set; } = new List<Leagueplayer>();

    public virtual ICollection<Leagueteam> Leagueteams { get; set; } = new List<Leagueteam>();

    public virtual ICollection<Playoff> Playoffs { get; set; } = new List<Playoff>();

    public virtual Statsvalue? Statsvalue { get; set; }

    public virtual ICollection<Transactionleague> Transactionleagues { get; set; } = new List<Transactionleague>();

    public virtual ICollection<Userleague> Userleagues { get; set; } = new List<Userleague>();
}

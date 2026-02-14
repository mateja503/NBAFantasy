using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Playoff
{
    public long Playoffid { get; set; }

    public int Totalrounds { get; set; }

    public long? Leagueid { get; set; }

    public virtual League? League { get; set; }

    public virtual ICollection<Playoffbracket> Playoffbrackets { get; set; } = new List<Playoffbracket>();
}

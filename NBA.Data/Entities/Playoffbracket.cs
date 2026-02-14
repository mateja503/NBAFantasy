using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Playoffbracket
{
    public long Playoffbracketid { get; set; }

    public int Playoffround { get; set; }

    public long Team1 { get; set; }

    public long Team2 { get; set; }

    public long? Playoffid { get; set; }

    public virtual Playoff? Playoff { get; set; }
}

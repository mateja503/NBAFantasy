using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Playermemento
{
    public long Playermemontoid { get; set; }

    public string? Playersteam { get; set; }

    public long? Points { get; set; }

    public long? Assists { get; set; }

    public long? Rebounds { get; set; }

    public long? Blocks { get; set; }

    public long? Steals { get; set; }

    public long? Threepointers { get; set; }

    public long? Turnovers { get; set; }

    public double? Freethrowperc { get; set; }

    public double? Fieldgoalperc { get; set; }

    public DateTime? Tscreated { get; set; }

    public virtual Player? Player { get; set; }
}

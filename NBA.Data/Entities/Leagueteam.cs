using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Leagueteam
{
    public long Leagueteamid { get; set; }

    public long? LeagueId { get; set; }

    public long? TeamId { get; set; }

    public virtual League? League { get; set; }

    public virtual Team? Team { get; set; }
}

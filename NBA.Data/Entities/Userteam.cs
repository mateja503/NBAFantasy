using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Userteam
{
    public long Userteamid { get; set; }

    public long Teamid { get; set; }

    public long Userid { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual Applicationuser User { get; set; } = null!;
}

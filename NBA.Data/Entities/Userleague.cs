using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Userleague
{
    public long Userleagueid { get; set; }

    public long Userid { get; set; }

    public long Leagueid { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Applicationuser User { get; set; } = null!;
}

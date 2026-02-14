using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Usertrophie
{
    public long Usertrophieid { get; set; }

    public long Userid { get; set; }

    public long Trophieid { get; set; }

    public virtual Trophie Trophie { get; set; } = null!;

    public virtual Applicationuser User { get; set; } = null!;
}

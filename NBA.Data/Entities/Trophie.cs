using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Trophie
{
    public long Trophieid { get; set; }

    public long? Xp { get; set; }

    public string? Typetrophie { get; set; }

    public virtual ICollection<Usertrophie> Usertrophies { get; set; } = new List<Usertrophie>();
}

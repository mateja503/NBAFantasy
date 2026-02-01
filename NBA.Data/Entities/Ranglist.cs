using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Ranglist
{
    public long Ranglistid { get; set; }

    public string Title { get; set; } = null!;

    public long Totalpoints { get; set; }

    public DateTime Tscreated { get; set; }

    public DateTime? Tsupdated { get; set; }

    public string Usercreated { get; set; } = null!;

    public string? Userupdated { get; set; }

    public virtual ICollection<Ranglistteam> Ranglistteams { get; set; } = new List<Ranglistteam>();

    public virtual ICollection<Ranglistuser> Ranglistusers { get; set; } = new List<Ranglistuser>();
}

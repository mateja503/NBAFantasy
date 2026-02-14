using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Applicationuser
{
    public long Userid { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public long? Xp { get; set; }

    public int? Managerlevel { get; set; }

    public virtual ICollection<Userleague> Userleagues { get; set; } = new List<Userleague>();

    public virtual ICollection<Userteam> Userteams { get; set; } = new List<Userteam>();

    public virtual ICollection<Usertrophie> Usertrophies { get; set; } = new List<Usertrophie>();
}

using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class User
{
    public long Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime Tscreated { get; set; }

    public DateTime? Tsupdated { get; set; }

    public string Usercreated { get; set; } = null!;

    public string? Userupdated { get; set; }

    public virtual ICollection<Ranglistuser> Ranglistusers { get; set; } = new List<Ranglistuser>();
}

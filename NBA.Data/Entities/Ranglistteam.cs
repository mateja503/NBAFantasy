using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Ranglistteam
{
    public long Rlteamid { get; set; }

    public long? RanglistId { get; set; }

    public long? TeamId { get; set; }

    public virtual Ranglist? Ranglist { get; set; }

    public virtual Team? Team { get; set; }
}

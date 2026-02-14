using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Transactionleague
{
    public long Transactionleagueid { get; set; }

    public long Transactionid { get; set; }

    public long Leagueid { get; set; }

    public virtual League League { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}

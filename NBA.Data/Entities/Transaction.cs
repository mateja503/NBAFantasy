using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Transaction
{
    public long Transactionid { get; set; }

    public DateTime? Tscreated { get; set; }

    public int? Typetransaction { get; set; }

    public int? Transactionstatus { get; set; }

    public virtual ICollection<Transactionleague> Transactionleagues { get; set; } = new List<Transactionleague>();
}

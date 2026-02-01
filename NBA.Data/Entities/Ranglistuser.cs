using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Ranglistuser
{
    public long Rluserid { get; set; }

    public long? RanglistId { get; set; }

    public long? UserId { get; set; }

    public virtual Ranglist? Ranglist { get; set; }

    public virtual User? User { get; set; }
}

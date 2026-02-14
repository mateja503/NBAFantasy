using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NBA.Data.Context;

public partial class NbaFantasyContext : DbContext
{
    public NbaFantasyContext(DbContextOptions<NbaFantasyContext> options)
        : base(options)
    {
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

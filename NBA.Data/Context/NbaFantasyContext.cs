using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NBA.Data.Entities;

namespace NBA.Data.Context;

public partial class NbaFantasyContext : DbContext
{
    public NbaFantasyContext(DbContextOptions<NbaFantasyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Leagueteam> Leagueteams { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Ranglist> Ranglists { get; set; }

    public virtual DbSet<Ranglistteam> Ranglistteams { get; set; }

    public virtual DbSet<Ranglistuser> Ranglistusers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Leagueid).HasName("league_pkey");

            entity.ToTable("league", "nba");

            entity.Property(e => e.Leagueid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("leagueid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Tscreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tsupdated");
            entity.Property(e => e.Usercreated)
                .HasMaxLength(255)
                .HasColumnName("usercreated");
            entity.Property(e => e.Userupdated)
                .HasMaxLength(255)
                .HasColumnName("userupdated");
        });

        modelBuilder.Entity<Leagueteam>(entity =>
        {
            entity.HasKey(e => e.Leagueteamid).HasName("leagueteam_pkey");

            entity.ToTable("leagueteam", "nba");

            entity.HasIndex(e => new { e.LeagueId, e.TeamId }, "leagueteam_league_id_team_id_key").IsUnique();

            entity.Property(e => e.Leagueteamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("leagueteamid");
            entity.Property(e => e.LeagueId).HasColumnName("league_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.League).WithMany(p => p.Leagueteams)
                .HasForeignKey(d => d.LeagueId)
                .HasConstraintName("leagueteam_league_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Leagueteams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("leagueteam_team_id_fkey");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Playerid).HasName("player_pkey");

            entity.ToTable("player", "nba");

            entity.HasIndex(e => new { e.Name, e.Teamid, e.Leagueid }, "unique_player_session").IsUnique();

            entity.Property(e => e.Playerid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("playerid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Tscreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tsupdated");
            entity.Property(e => e.Usercreated)
                .HasMaxLength(255)
                .HasColumnName("usercreated");
            entity.Property(e => e.Userupdated)
                .HasMaxLength(255)
                .HasColumnName("userupdated");

            entity.HasOne(d => d.League).WithMany(p => p.Players)
                .HasForeignKey(d => d.Leagueid)
                .HasConstraintName("player_leagueid_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.Teamid)
                .HasConstraintName("player_teamid_fkey");
        });

        modelBuilder.Entity<Ranglist>(entity =>
        {
            entity.HasKey(e => e.Ranglistid).HasName("ranglist_pkey");

            entity.ToTable("ranglist", "nba");

            entity.Property(e => e.Ranglistid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ranglistid");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Totalpoints).HasColumnName("totalpoints");
            entity.Property(e => e.Tscreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tsupdated");
            entity.Property(e => e.Usercreated)
                .HasMaxLength(255)
                .HasColumnName("usercreated");
            entity.Property(e => e.Userupdated)
                .HasMaxLength(255)
                .HasColumnName("userupdated");
        });

        modelBuilder.Entity<Ranglistteam>(entity =>
        {
            entity.HasKey(e => e.Rlteamid).HasName("ranglistteam_pkey");

            entity.ToTable("ranglistteam", "nba");

            entity.HasIndex(e => new { e.RanglistId, e.TeamId }, "ranglistteam_ranglist_id_team_id_key").IsUnique();

            entity.Property(e => e.Rlteamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("rlteamid");
            entity.Property(e => e.RanglistId).HasColumnName("ranglist_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Ranglist).WithMany(p => p.Ranglistteams)
                .HasForeignKey(d => d.RanglistId)
                .HasConstraintName("ranglistteam_ranglist_id_fkey");

            entity.HasOne(d => d.Team).WithMany(p => p.Ranglistteams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("ranglistteam_team_id_fkey");
        });

        modelBuilder.Entity<Ranglistuser>(entity =>
        {
            entity.HasKey(e => e.Rluserid).HasName("ranglistuser_pkey");

            entity.ToTable("ranglistuser", "nba");

            entity.HasIndex(e => new { e.RanglistId, e.UserId }, "ranglistuser_ranglist_id_user_id_key").IsUnique();

            entity.Property(e => e.Rluserid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("rluserid");
            entity.Property(e => e.RanglistId).HasColumnName("ranglist_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ranglist).WithMany(p => p.Ranglistusers)
                .HasForeignKey(d => d.RanglistId)
                .HasConstraintName("ranglistuser_ranglist_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ranglistusers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("ranglistuser_user_id_fkey");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("team_pkey");

            entity.ToTable("team", "nba");

            entity.Property(e => e.Teamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teamid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Tscreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tsupdated");
            entity.Property(e => e.Usercreated)
                .HasMaxLength(255)
                .HasColumnName("usercreated");
            entity.Property(e => e.Userupdated)
                .HasMaxLength(255)
                .HasColumnName("userupdated");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("user_pkey");

            entity.ToTable("user", "nba");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userid");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Tscreated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("tsupdated");
            entity.Property(e => e.Usercreated)
                .HasMaxLength(255)
                .HasColumnName("usercreated");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Userupdated)
                .HasMaxLength(255)
                .HasColumnName("userupdated");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

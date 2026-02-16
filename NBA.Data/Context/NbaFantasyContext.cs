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

    public virtual DbSet<Applicationuser> Applicationusers { get; set; }

    public virtual DbSet<League> Leagues { get; set; }

    public virtual DbSet<Leagueplayer> Leagueplayers { get; set; }

    public virtual DbSet<Leagueteam> Leagueteams { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Playermemento> Playermementos { get; set; }

    public virtual DbSet<Playoff> Playoffs { get; set; }

    public virtual DbSet<Playoffbracket> Playoffbrackets { get; set; }

    public virtual DbSet<Statsvalue> Statsvalues { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Teamplayer> Teamplayers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transactionleague> Transactionleagues { get; set; }

    public virtual DbSet<Trophie> Trophies { get; set; }

    public virtual DbSet<Userleague> Userleagues { get; set; }

    public virtual DbSet<Userteam> Userteams { get; set; }

    public virtual DbSet<Usertrophie> Usertrophies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicationuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("applicationuser_pkey");

            entity.ToTable("applicationuser", "nba");

            entity.Property(e => e.Userid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Managerlevel).HasColumnName("managerlevel");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
            entity.Property(e => e.Xp)
                .HasDefaultValue(0L)
                .HasColumnName("xp");
        });

        modelBuilder.Entity<League>(entity =>
        {
            entity.HasKey(e => e.Leagueid).HasName("league_pkey");

            entity.ToTable("league", "nba");

            entity.HasIndex(e => e.Statsvalueid, "league_statsvalueid_key").IsUnique();

            entity.Property(e => e.Leagueid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("leagueid");
            entity.Property(e => e.Autostart).HasColumnName("autostart");
            entity.Property(e => e.Commissioner).HasColumnName("commissioner");
            entity.Property(e => e.Draftstyle).HasColumnName("draftstyle");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Seasonyear)
                .HasMaxLength(255)
                .HasColumnName("seasonyear");
            entity.Property(e => e.Statsvalueid).HasColumnName("statsvalueid");
            entity.Property(e => e.Transactionlimit).HasColumnName("transactionlimit");
            entity.Property(e => e.Typeleague).HasColumnName("typeleague");
            entity.Property(e => e.Typetransactionlimits).HasColumnName("typetransactionlimits");
            entity.Property(e => e.Weeksforseason).HasColumnName("weeksforseason");

            entity.HasOne(d => d.Statsvalue).WithOne(p => p.League)
                .HasForeignKey<League>(d => d.Statsvalueid)
                .HasConstraintName("league_statsvalueid_fkey");
        });

        modelBuilder.Entity<Leagueplayer>(entity =>
        {
            entity.HasKey(e => e.Leagueplayerid).HasName("leagueplayer_pkey");

            entity.ToTable("leagueplayer", "nba");

            entity.Property(e => e.Leagueplayerid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("leagueplayerid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Playerid).HasColumnName("playerid");

            entity.HasOne(d => d.League).WithMany(p => p.Leagueplayers)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leagueplayer_league");

            entity.HasOne(d => d.Player).WithMany(p => p.Leagueplayers)
                .HasForeignKey(d => d.Playerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leagueplayer_player");
        });

        modelBuilder.Entity<Leagueteam>(entity =>
        {
            entity.HasKey(e => e.Leagueteamid).HasName("leagueteam_pkey");

            entity.ToTable("leagueteam", "nba");

            entity.Property(e => e.Leagueteamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("leagueteamid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");

            entity.HasOne(d => d.League).WithMany(p => p.Leagueteams)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leagueteam_league");

            entity.HasOne(d => d.Team).WithMany(p => p.Leagueteams)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_leagueteam_team");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Playerid).HasName("player_pkey");

            entity.ToTable("player", "nba");

            entity.HasIndex(e => e.Playermemontoid, "player_playermemontoid_key").IsUnique();

            entity.Property(e => e.Playerid)
                .ValueGeneratedNever()
                .HasColumnName("playerid");
            entity.Property(e => e.Allowdrop)
                .HasDefaultValue(false)
                .HasColumnName("allowdrop");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0L)
                .HasColumnName("assists");
            entity.Property(e => e.Blocks)
                .HasDefaultValue(0L)
                .HasColumnName("blocks");
            entity.Property(e => e.Fieldgoalperc).HasColumnName("fieldgoalperc");
            entity.Property(e => e.Freethrowperc).HasColumnName("freethrowperc");
            entity.Property(e => e.Gameready).HasColumnName("gameready");
            entity.Property(e => e.Irlteamid).HasColumnName("irlteamid");
            entity.Property(e => e.Irlteamname)
                .HasMaxLength(100)
                .HasColumnName("irlteamname");
            entity.Property(e => e.Isdrop)
                .HasDefaultValue(false)
                .HasColumnName("isdrop");
            entity.Property(e => e.Isfreeagent)
                .HasDefaultValue(true)
                .HasColumnName("isfreeagent");
            entity.Property(e => e.Islock)
                .HasDefaultValue(false)
                .HasColumnName("islock");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Playermemontoid).HasColumnName("playermemontoid");
            entity.Property(e => e.Playerposition).HasColumnName("playerposition");
            entity.Property(e => e.Points)
                .HasDefaultValue(0L)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0L)
                .HasColumnName("rebounds");
            entity.Property(e => e.Rosterrole).HasColumnName("rosterrole");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0L)
                .HasColumnName("steals");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .HasColumnName("surname");
            entity.Property(e => e.Threepointers)
                .HasDefaultValue(0L)
                .HasColumnName("threepointers");
            entity.Property(e => e.Tscreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("tscreated");
            entity.Property(e => e.Tsupdated).HasColumnName("tsupdated");
            entity.Property(e => e.Turnovers)
                .HasDefaultValue(0L)
                .HasColumnName("turnovers");

            entity.HasOne(d => d.Playermemonto).WithOne(p => p.Player)
                .HasForeignKey<Player>(d => d.Playermemontoid)
                .HasConstraintName("player_playermemontoid_fkey");
        });

        modelBuilder.Entity<Playermemento>(entity =>
        {
            entity.HasKey(e => e.Playermemontoid).HasName("playermemento_pkey");

            entity.ToTable("playermemento", "nba");

            entity.Property(e => e.Playermemontoid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("playermemontoid");
            entity.Property(e => e.Assists)
                .HasDefaultValue(0L)
                .HasColumnName("assists");
            entity.Property(e => e.Blocks)
                .HasDefaultValue(0L)
                .HasColumnName("blocks");
            entity.Property(e => e.Fieldgoalperc).HasColumnName("fieldgoalperc");
            entity.Property(e => e.Freethrowperc).HasColumnName("freethrowperc");
            entity.Property(e => e.Playersteam)
                .HasMaxLength(100)
                .HasColumnName("playersteam");
            entity.Property(e => e.Points)
                .HasDefaultValue(0L)
                .HasColumnName("points");
            entity.Property(e => e.Rebounds)
                .HasDefaultValue(0L)
                .HasColumnName("rebounds");
            entity.Property(e => e.Steals)
                .HasDefaultValue(0L)
                .HasColumnName("steals");
            entity.Property(e => e.Threepointers)
                .HasDefaultValue(0L)
                .HasColumnName("threepointers");
            entity.Property(e => e.Tscreated).HasColumnName("tscreated");
            entity.Property(e => e.Turnovers)
                .HasDefaultValue(0L)
                .HasColumnName("turnovers");
        });

        modelBuilder.Entity<Playoff>(entity =>
        {
            entity.HasKey(e => e.Playoffid).HasName("playoff_pkey");

            entity.ToTable("playoff", "nba");

            entity.Property(e => e.Playoffid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("playoffid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Totalrounds)
                .HasDefaultValue(4)
                .HasColumnName("totalrounds");

            entity.HasOne(d => d.League).WithMany(p => p.Playoffs)
                .HasForeignKey(d => d.Leagueid)
                .HasConstraintName("fk_playoff_league");
        });

        modelBuilder.Entity<Playoffbracket>(entity =>
        {
            entity.HasKey(e => e.Playoffbracketid).HasName("playoffbracket_pkey");

            entity.ToTable("playoffbracket", "nba");

            entity.Property(e => e.Playoffbracketid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("playoffbracketid");
            entity.Property(e => e.Playoffid).HasColumnName("playoffid");
            entity.Property(e => e.Playoffround)
                .HasDefaultValue(1)
                .HasColumnName("playoffround");
            entity.Property(e => e.Team1).HasColumnName("team1");
            entity.Property(e => e.Team2).HasColumnName("team2");

            entity.HasOne(d => d.Playoff).WithMany(p => p.Playoffbrackets)
                .HasForeignKey(d => d.Playoffid)
                .HasConstraintName("fk_bracket_playoff");
        });

        modelBuilder.Entity<Statsvalue>(entity =>
        {
            entity.HasKey(e => e.Statsvalueid).HasName("statsvalue_pkey");

            entity.ToTable("statsvalue", "nba");

            entity.Property(e => e.Statsvalueid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("statsvalueid");
            entity.Property(e => e.Assistsvalue).HasColumnName("assistsvalue");
            entity.Property(e => e.Blocksvalue).HasColumnName("blocksvalue");
            entity.Property(e => e.Fieldgoalpercvalue).HasColumnName("fieldgoalpercvalue");
            entity.Property(e => e.Freethrowpervalue).HasColumnName("freethrowpervalue");
            entity.Property(e => e.Pointsvalue).HasColumnName("pointsvalue");
            entity.Property(e => e.Reboundsvalue).HasColumnName("reboundsvalue");
            entity.Property(e => e.Threepointsvalue).HasColumnName("threepointsvalue");
            entity.Property(e => e.Turnoversvalue).HasColumnName("turnoversvalue");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Teamid).HasName("team_pkey");

            entity.ToTable("team", "nba");

            entity.Property(e => e.Teamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teamid");
            entity.Property(e => e.Categoryleaguepoints)
                .HasDefaultValue(0.0)
                .HasColumnName("categoryleaguepoints");
            entity.Property(e => e.Islock)
                .HasDefaultValue(false)
                .HasColumnName("islock");
            entity.Property(e => e.Lastweekpoints)
                .HasDefaultValue(0.0)
                .HasColumnName("lastweekpoints");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Seed).HasColumnName("seed");
            entity.Property(e => e.Waiverpriority).HasColumnName("waiverpriority");
        });

        modelBuilder.Entity<Teamplayer>(entity =>
        {
            entity.HasKey(e => e.Teamplayerid).HasName("teamplayer_pkey");

            entity.ToTable("teamplayer", "nba");

            entity.Property(e => e.Teamplayerid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("teamplayerid");
            entity.Property(e => e.Playerid).HasColumnName("playerid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");

            entity.HasOne(d => d.Player).WithMany(p => p.Teamplayers)
                .HasForeignKey(d => d.Playerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_teamplayer_player");

            entity.HasOne(d => d.Team).WithMany(p => p.Teamplayers)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_teamplayer_team");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Transactionid).HasName("transactions_pkey");

            entity.ToTable("transactions", "nba");

            entity.Property(e => e.Transactionid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("transactionid");
            entity.Property(e => e.Transactionstatus).HasColumnName("transactionstatus");
            entity.Property(e => e.Tscreated).HasColumnName("tscreated");
            entity.Property(e => e.Typetransaction).HasColumnName("typetransaction");
        });

        modelBuilder.Entity<Transactionleague>(entity =>
        {
            entity.HasKey(e => e.Transactionleagueid).HasName("transactionleague_pkey");

            entity.ToTable("transactionleague", "nba");

            entity.Property(e => e.Transactionleagueid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("transactionleagueid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Transactionid).HasColumnName("transactionid");

            entity.HasOne(d => d.League).WithMany(p => p.Transactionleagues)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trans_league");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Transactionleagues)
                .HasForeignKey(d => d.Transactionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trans_id");
        });

        modelBuilder.Entity<Trophie>(entity =>
        {
            entity.HasKey(e => e.Trophieid).HasName("trophie_pkey");

            entity.ToTable("trophie", "nba");

            entity.Property(e => e.Trophieid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("trophieid");
            entity.Property(e => e.Typetrophie)
                .HasMaxLength(255)
                .HasColumnName("typetrophie");
            entity.Property(e => e.Xp)
                .HasDefaultValue(25L)
                .HasColumnName("xp");
        });

        modelBuilder.Entity<Userleague>(entity =>
        {
            entity.HasKey(e => e.Userleagueid).HasName("userleague_pkey");

            entity.ToTable("userleague", "nba");

            entity.Property(e => e.Userleagueid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userleagueid");
            entity.Property(e => e.Leagueid).HasColumnName("leagueid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.League).WithMany(p => p.Userleagues)
                .HasForeignKey(d => d.Leagueid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userleague_league");

            entity.HasOne(d => d.User).WithMany(p => p.Userleagues)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userleague_user");
        });

        modelBuilder.Entity<Userteam>(entity =>
        {
            entity.HasKey(e => e.Userteamid).HasName("userteam_pkey");

            entity.ToTable("userteam", "nba");

            entity.Property(e => e.Userteamid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("userteamid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Team).WithMany(p => p.Userteams)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userteam_team");

            entity.HasOne(d => d.User).WithMany(p => p.Userteams)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userteam_user");
        });

        modelBuilder.Entity<Usertrophie>(entity =>
        {
            entity.HasKey(e => e.Usertrophieid).HasName("usertrophie_pkey");

            entity.ToTable("usertrophie", "nba");

            entity.Property(e => e.Usertrophieid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("usertrophieid");
            entity.Property(e => e.Trophieid).HasColumnName("trophieid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Trophie).WithMany(p => p.Usertrophies)
                .HasForeignKey(d => d.Trophieid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usertrophie_trophie");

            entity.HasOne(d => d.User).WithMany(p => p.Usertrophies)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usertrophie_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

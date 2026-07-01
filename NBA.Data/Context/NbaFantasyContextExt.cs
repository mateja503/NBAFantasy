

using Microsoft.EntityFrameworkCore;
using NBA.Data.Entities;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NBA.Data.Context
{
    public partial class NbaFantasyContext{

        //public virtual DbSet<Draftsnapshot> Draftsnapshots { get; set; }

        // Configured here (in the partial) rather than the scaffolded file so a future re-scaffold
        // of the database-first model doesn't clobber it.
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Draftsnapshot>(entity =>
        //    {
        //        entity.HasKey(e => e.Leagueid).HasName("draftsnapshot_pkey");
        //        entity.ToTable("draftsnapshot", "nba");
        //        entity.Property(e => e.Leagueid).ValueGeneratedNever().HasColumnName("leagueid");
        //        entity.Property(e => e.Draftstate).HasColumnName("draftstate");
        //        entity.Property(e => e.Draftteams).HasColumnName("draftteams");
        //        entity.Property(e => e.Tsupdated).HasColumnName("tsupdated");
        //    });
        //}

        #region DraftSnapshot
        public async Task<Draftsnapshot?> GetDraftSnapshot(long leagueId)
        {
            return await Draftsnapshots.AsNoTracking().SingleOrDefaultAsync(s => s.Leagueid == leagueId);
        }

        public async Task UpsertDraftSnapshot(Draftsnapshot snapshot)
        {
            var existing = await Draftsnapshots.SingleOrDefaultAsync(s => s.Leagueid == snapshot.Leagueid);
            if (existing is null)
            {
                await Draftsnapshots.AddAsync(snapshot);
            }
            else
            {
                existing.Draftstate = snapshot.Draftstate;
                existing.Draftteams = snapshot.Draftteams;
                existing.Tsupdated = snapshot.Tsupdated;
            }
            _ = await SaveChangesAsync();
        }

        public async Task DeleteDraftSnapshot(long leagueId)
        {
            var existing = await Draftsnapshots.SingleOrDefaultAsync(s => s.Leagueid == leagueId);
            if (existing is not null)
            {
                Draftsnapshots.Remove(existing);
                _ = await SaveChangesAsync();
            }
        }
        #endregion

         #region Players
        public IQueryable<Player> GetAllPlayers()
        {
            return Players.AsQueryable();
        }

        public async Task AddPlayers(List<Player> players,CancellationToken cancellationToken)
        {
            await Players.AddRangeAsync(players, cancellationToken);
            _ = await SaveChangesAsync();
        }
        public async Task UpdatePlayersRange(List<Player> players, CancellationToken cancellationToken = default)
        {
            Players.UpdateRange(players);
            _ = await SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region TeamPlayer
        public IQueryable<Teamplayer> GetAllTeamPlayer() 
        {
            return Teamplayers.AsQueryable();
        }
        public async Task<Teamplayer> AddTeamPlayer(Teamplayer teamplayer) 
        {
            var e = await Teamplayers.AddAsync(teamplayer);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        public async Task<List<Teamplayer>> DeleteTeamPlayerRange(List<Teamplayer> teamplayers) 
        {
            Teamplayers.RemoveRange(teamplayers);
            _ = await SaveChangesAsync();
            return teamplayers;
            
        }

        public async Task<List<Teamplayer>> AddTeamPlayerRange(List<Teamplayer> teamplayers) 
        {
            await Teamplayers.AddRangeAsync(teamplayers);
            _ = await SaveChangesAsync();
            return teamplayers;
        }
        #endregion

        #region LeaguePlayer
        public IQueryable<Leagueplayer> GetAllLeaguePlayers() 
        {
            return Leagueplayers.AsQueryable();
        }

        public async Task<List<Leagueplayer>> UpdatLeaguePlayersRange(List<Leagueplayer> leagueplayers) 
        {
            Leagueplayers.UpdateRange(leagueplayers);
            _ = await SaveChangesAsync();
            return leagueplayers;
        }
        #endregion

      
        #region Team
        public IQueryable<Team> GetAllTeams()
        {
            return Teams.AsQueryable();
        }
        public async Task<Team> AddTeam(Team entity) 
        {
            var e = await Teams.AddAsync(entity);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        public async Task<List<Team>> AddTeamRange(List<Team> teams) 
        {
            await Teams.AddRangeAsync(teams);
            _ = await SaveChangesAsync();
            return teams;
        }
        #endregion

        #region League
        public IQueryable<League> GetAllLeagues() 
        {
            return Leagues.AsQueryable();
        }

        public async Task<League> AddLeague(League league)
        {
            var e = await Leagues.AddAsync(league);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        public async Task<League> UpdateLeague(League league) 
        {
            var e = Leagues.Update(league);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        #endregion

        #region StatsValue
        public IQueryable<Statsvalue> GetAllStatsValues() 
        {
            return Statsvalues.AsQueryable();
        }
        public async Task<Statsvalue> AddStatsValue(Statsvalue statsvalue) 
        {
            var e = await Statsvalues.AddAsync(statsvalue);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region Authentication
        public async Task<Applicationuser?> GetApplicationuser(string username, string password)
        {
            return await Applicationusers.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        // Credentials are now verified against a hashed password in the service layer,
        // so lookups are by username only.
        public async Task<Applicationuser?> GetApplicationuserByUsername(string username)
        {
            return await Applicationusers.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Applicationuser?> GetApplicationuserById(long userId)
        {
            return await Applicationusers.SingleOrDefaultAsync(u => u.Userid == userId);
        }

        public async Task<Applicationuser> AddApplicationuser(Applicationuser user)
        {
            var e = await Applicationusers.AddAsync(user);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        public async Task<Applicationuser> UpdateApplicationuser(Applicationuser user)
        {
            var e = Applicationusers.Update(user);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        #endregion
    }
}

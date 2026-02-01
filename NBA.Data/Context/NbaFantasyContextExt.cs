
using Microsoft.EntityFrameworkCore;
using NBA.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NBA.Data.Context
{
    public partial class NbaFantasyContext
    {

        #region League
        public IQueryable<League> GetAllLeagues()
        {
            return Leagues.AsNoTracking().AsQueryable();
        }
        public async Task<League> AddLeageue(League league) 
        {
            var e = await Leagues.AddAsync(league);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<League> UpdateLeage(League league) 
        {
            var e = Leagues.Update(league);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region Player
        public IQueryable<Player> GetAllPlayers()
        {
            return Players.AsNoTracking().AsQueryable();
        }
        public async Task<Player> AddPlayer(Player player)
        {
            var e = await Players.AddAsync(player);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<Player> UpdatePlayer(Player player)
        {
            var e = Players.Update(player);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region RangList
        public IQueryable<Ranglist> GetAllRankLists()
        {
            return Ranglists.AsNoTracking().AsQueryable();
        }

        public async Task<Ranglist> AddRankList(Ranglist rankList)
        {
            var e = await Ranglists.AddAsync(rankList);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<Ranglist> UpdateRankList(Ranglist rankList)
        {
            var e = Ranglists.Update(rankList);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        #endregion

        #region Team
        public IQueryable<Team> GetAllTeams()
        {
            return Teams.AsNoTracking().AsQueryable();
        }
        public async Task<Team> AddTeam(Team team)
        {
            var e = await Teams.AddAsync(team);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<Team> UpdateTeam(Team team)
        {
            var e = Teams.Update(team);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region LeageueTeam
        public IQueryable<Leagueteam> GetAllLeagueTeams()
        {
            return Leagueteams.AsNoTracking().AsQueryable();
        }
        public async Task<Leagueteam> AddLeagueTeam(Leagueteam leagueTeam)
        {
            var e = await Leagueteams.AddAsync(leagueTeam);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<Leagueteam> UpdateLeagueTeam(Leagueteam leagueTeam)
        {
            var e = Leagueteams.Update(leagueTeam);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region RanglistTeam
        public IQueryable<Ranglistteam> GetAllRanglistTeams()
        {
            return Ranglistteams.AsNoTracking().AsQueryable();
        }   
        public async Task<Ranglistteam> AddRanglistTeam(Ranglistteam ranglistTeam)
        {
            var e = await Ranglistteams.AddAsync(ranglistTeam);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        public async Task<Ranglistteam> UpdateRanglistTeam(Ranglistteam ranglistTeam)
        {
            var e = Ranglistteams.Update(ranglistTeam);
            _ = await SaveChangesAsync();
            return e.Entity;
        }
        #endregion

        #region RangListUser

        public IQueryable<Ranglistuser> GetRanglistusers() 
        {
            return Ranglistusers.AsNoTracking().AsQueryable();
        }
        public async Task<Ranglistuser> AddRanglistuser(Ranglistuser ranglistuser) 
        {
            var e = await Ranglistusers.AddAsync(ranglistuser);
            _ = await SaveChangesAsync();
            return e.Entity;
        }   
        public async Task<Ranglistuser> UpdateRanglistuser(Ranglistuser ranglistuser) 
        {
            var e = Ranglistusers.Update(ranglistuser);
            _ = await SaveChangesAsync();
            return e.Entity;
        }

        #endregion
    }


}

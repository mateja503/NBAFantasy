

using NBA.Data.Entities;
using System.Net;
using System.Runtime.CompilerServices;

namespace NBA.Data.Context
{
    public partial class NbaFantasyContext{

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

        #region LeagueTeam
        public IQueryable<Leagueteam> GetAllLeagueTeam() 
        {
            return Leagueteams.AsQueryable();
        }
        #endregion

        #region Team         
        public IQueryable<Team> GetAllTeams() 
        {
            return Teams.AsQueryable();
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
    }
}



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
    }
}

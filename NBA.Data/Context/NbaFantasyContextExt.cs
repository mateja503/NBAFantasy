

using NBA.Data.Entities;
using System.Net;

namespace NBA.Data.Context
{
    public partial class NbaFantasyContext{

    #region Players
        public IQueryable<Player> GetAllPlayers()
        {
            return Players.AsQueryable();
        }

        public async Task AddPlayers(List<Player> players)
        {
            await Players.AddRangeAsync(players);
            _ = await SaveChangesAsync();
        }
    #endregion
    }
}

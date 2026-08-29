using NBA.Data.Redis.Entities;
using TradeData = NBA.Data.Entities.Trade;

namespace NBA.Service.Trade
{
    // The seam TradeHub depends on. It exists so the hub can be constructed — and therefore tested —
    // without a live Postgres and Redis behind it: previously the hub named four concrete collaborators,
    // each of which transitively demanded both stores.
    //
    // Deliberately the only interface introduced in the trade path. TradeService and TradeManager are
    // left concrete: they are the things a fake orchestrator replaces, so adding interfaces there would
    // buy nothing that this one does not already buy.
    public interface ITradeOrchestrator
    {
        Task<TradeOutcome<TradeData>> ProposeAsync(long leagueId, long fromTeam, long toTeam, List<long> playersIds);

        Task<TradeOutcome<TradeData>> AcceptAsync(long leagueId, Guid tradeId);

        Task<TradeOutcome<TradeData>> RejectAsync(long leagueId, Guid tradeId);

        Task<List<TradeBetweenTeams>> GetBacklogAsync(long leagueId, long teamId);
    }
}

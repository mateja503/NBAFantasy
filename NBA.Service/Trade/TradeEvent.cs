using NBA.Data.Redis.Entities;

namespace NBA.Service.Trade
{
    // What a trade operation caused, expressed as data rather than as a broadcast.
    //
    // This is what lets the trade rules live in NBA.Service without NBA.Service having to know SignalR
    // exists: the orchestrator decides WHAT happened, TradeHub decides WHERE it is sent. Keeping the
    // dependency pointing that way is deliberate — NBA.Service referencing IHubContext would mean
    // NBA.Service referencing NBA.Api, the one direction the project's layering does not have.
    public abstract record TradeEvent
    {
        // Closes the hierarchy. An abstract record's generated constructor is protected, so without this
        // any type — in this assembly or another — could derive a fifth case, and TradeHub.Publish has no
        // send wired up for one it has never heard of. A private constructor is reachable only from
        // inside this type, which is exactly the nested cases below: that is what makes "a TradeEvent is
        // one of these four things" true rather than merely intended.
        private TradeEvent() { }

        // A new offer. Sent league-wide: the trade board shows every open offer in the league, not only
        // the ones aimed at the viewer.

        public sealed record OfferedToLeague(TradeBetweenTeams Trade) : TradeEvent;

        public sealed record Accepted(TradeBetweenTeams Trade) : TradeEvent;

        public sealed record Rejected(TradeBetweenTeams Trade) : TradeEvent;

        // The proposer replaced this offer with a newer one to the same team. Distinct from Rejected
        // because nobody declined it — a board that said "declined" would misread what happened.
        public sealed record Superseded(TradeBetweenTeams Trade) : TradeEvent;
    }

    // An operation's result together with everything it wants announced, in the order it should be
    // announced. Order matters: a supersede has to reach the board before the proposal that displaced
    // it, or a client processing them in arrival order shows the dead offer as the live one.
    public sealed record TradeOutcome<T>(T Result, IReadOnlyList<TradeEvent> Events);
}

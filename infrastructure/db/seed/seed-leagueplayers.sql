-- Seeds nba.leagueplayer for league 1: one row per player in nba.player, so league 1 starts with
-- the full pool the way LeagueService seeds it when a league is created for real.
--
-- isfreeagent is FALSE only for players already rostered in league 1 -- i.e. players sitting in
-- nba.teamplayer under a team whose leagueid = 1 (seed-teamplayers writes those). Everyone else
-- stays TRUE and is available in free agency.
--
-- Depends on seed-players, seed-leagues, seed-teams and seed-teamplayers having run first.
DO $$
DECLARE
    target_league CONSTANT BIGINT := 1;
    inserted_count INT;
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'nba'
        AND table_name = 'leagueplayer'
    ) THEN
        RAISE NOTICE 'Table nba.leagueplayer does not exist. No action taken.';
        RETURN;
    END IF;

    -- leagueplayer.leagueid is a foreign key, so a missing league would abort the whole init run.
    IF NOT EXISTS (SELECT 1 FROM nba.league WHERE leagueid = target_league) THEN
        RAISE NOTICE 'League % does not exist. No action taken.', target_league;
        RETURN;
    END IF;

    INSERT INTO nba.leagueplayer (playerid, leagueid, isfreeagent)
    SELECT
        p.playerid,
        target_league,
        NOT EXISTS (
            SELECT 1
            FROM nba.teamplayer tp
            JOIN nba.team t ON t.teamid = tp.teamid
            WHERE tp.playerid = p.playerid
            AND t.leagueid = target_league
        )
    FROM nba.player p
    -- uq_leagueplayer_league_player already guarantees one row per (league, player); DO NOTHING
    -- rather than DO UPDATE so re-running this against a live database cannot undo signings or
    -- drops that happened after the seed.
    ON CONFLICT (leagueid, playerid) DO NOTHING;

    GET DIAGNOSTICS inserted_count = ROW_COUNT;
    RAISE NOTICE '% rows successfully inserted into nba.leagueplayer for league %.', inserted_count, target_league;
END $$;

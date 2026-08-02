-- Seeds nba.teamplayer: the rosters that a completed draft would normally write.
--
-- Rather than hard-coding a few hundred (teamid, playerid) pairs, players are handed out
-- deterministically: teams are numbered within their league, and each team takes the next
-- slice of the player pool. That means
--   * no player is on two teams in the SAME league (each slot takes a distinct slice), and
--   * every league drafts from the full pool again (the slice restarts per league),
-- which is exactly how a real draft behaves.
--
-- Roster size matches ApplicationSettings: Draft:Rounds = 12 picks per team, split 9 non-centers
-- + 3 centers so it stays under CenterLimit (4) and MaxPlayersPerTeam (13).
DO $$
DECLARE
    roster_non_centers CONSTANT INT := 9;
    roster_centers     CONSTANT INT := 3;
    center_position    CONSTANT INT := 3;  -- PlayerPositionEnum.C
    inserted_count INT;
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'nba'
        AND table_name = 'teamplayer'
    ) THEN
        RAISE NOTICE 'Table nba.teamplayer does not exist. No action taken.';
        RETURN;
    END IF;

    WITH team_slot AS (
        SELECT
            teamid,
            leagueid,
            ROW_NUMBER() OVER (PARTITION BY leagueid ORDER BY teamid) - 1 AS slot
        FROM nba.team
        WHERE leagueid IS NOT NULL
    ),
    -- Two pools, so the centers per team can be capped independently of everyone else.
    -- A NULL position counts as a non-center.
    non_center AS (
        SELECT playerid, ROW_NUMBER() OVER (ORDER BY playerid) - 1 AS rn
        FROM nba.player
        WHERE playerposition IS DISTINCT FROM center_position
    ),
    center AS (
        SELECT playerid, ROW_NUMBER() OVER (ORDER BY playerid) - 1 AS rn
        FROM nba.player
        WHERE playerposition = center_position
    ),
    roster AS (
        SELECT t.teamid, p.playerid
        FROM team_slot t
        CROSS JOIN generate_series(0, roster_non_centers - 1) AS i(idx)
        JOIN non_center p ON p.rn = t.slot * roster_non_centers + i.idx

        UNION ALL

        SELECT t.teamid, p.playerid
        FROM team_slot t
        CROSS JOIN generate_series(0, roster_centers - 1) AS j(idx)
        JOIN center p ON p.rn = t.slot * roster_centers + j.idx
    )
    INSERT INTO nba.teamplayer (teamid, playerid)
    SELECT r.teamid, r.playerid
    FROM roster r
    -- teamplayer has no unique constraint on (teamid, playerid), so re-running the seed is
    -- made safe here instead of with ON CONFLICT.
    WHERE NOT EXISTS (
        SELECT 1
        FROM nba.teamplayer tp
        WHERE tp.teamid = r.teamid
        AND tp.playerid = r.playerid
    );

    GET DIAGNOSTICS inserted_count = ROW_COUNT;
    RAISE NOTICE '% rows successfully inserted into nba.teamplayer.', inserted_count;
END $$;

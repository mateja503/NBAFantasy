DO $$
BEGIN
    IF EXISTS (
        SELECT FROM information_schema.tables 
        WHERE table_schema = 'nba' 
        AND table_name   = 'leagueteam'
    ) THEN
        INSERT INTO nba.leagueteam (
			teamid,
			leagueid,
			approved
        ) 
        VALUES 
            (1, 1, true),
            (2, 1, true),
            (3, 1, true),
            (4, 1, true),
            (5, 1, true),
            (6, 1, true),
            (7, 2, true),
            (8, 3, true);
            
        RAISE NOTICE '8 rows successfully inserted into nba.leagueteam.';
    ELSE
        RAISE NOTICE 'Table nba.leagueteam does not exist. No action taken.';
    END IF;
END $$;
DO $$
BEGIN
    IF EXISTS (
        SELECT FROM information_schema.tables 
        WHERE table_schema = 'nba' 
        AND table_name   = 'team'
    ) THEN
        INSERT INTO nba.team (
			name,
			seed,
			waiverpriority,
			lastweekpoints,
			categoryleaguepoints,
			islock
        ) 
        VALUES 
            ('Bulls', 1, 1,0,0,false),
            ('Warriors', 2, 1,0,0,false),
            ('Boston', 3, 1,0,0,false),
            ('Knicks', 4, 1,0,0,false),
            ('Atlanta', 5, 1,0,0,false),
            ('Kumanovo', 6, 1,0,0,false),
            ('Skopje', 1, 1,0,0,false),
            ('SRB', 1, 1,0,0,false);
            
        RAISE NOTICE '8 rows successfully inserted into nba.team.';
    ELSE
        RAISE NOTICE 'Table nba.team does not exist. No action taken.';
    END IF;
END $$;
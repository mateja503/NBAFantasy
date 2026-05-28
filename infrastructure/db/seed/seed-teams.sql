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
			islock,
			userid
        ) 
        VALUES 
            ('Bulls', 1, 1,0,0,false,1),
            ('Clevland', 1, 1,0,0,false,1),
            ('Warriors', 2, 1,0,0,false,2),
            ('Boston', 3, 1,0,0,false,3),
            ('Knicks', 4, 1,0,0,false,4),
            ('Atlanta', 5, 1,0,0,false,5),
            ('Kumanovo', 6, 1,0,0,false,6),
            ('Skopje', 1, 1,0,0,false,7),
            ('SRB', 1, 1,0,0,false,8);
            
        RAISE NOTICE '8 rows successfully inserted into nba.team.';
    ELSE
        RAISE NOTICE 'Table nba.team does not exist. No action taken.';
    END IF;
END $$;
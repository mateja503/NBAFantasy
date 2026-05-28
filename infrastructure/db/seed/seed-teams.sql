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
			userid,
			approved,
			leagueid
        ) 
        VALUES 
            ('Bulls', 1, 1,0,0,false,1,false,1),
            ('Warriors', 2, 1,0,0,false,2,false,1),
            ('Boston', 3, 1,0,0,false,3,false,1),
            ('Knicks', 4, 1,0,0,false,4,false,1),
            ('Atlanta', 5, 1,0,0,false,5,false,1),
            ('Kumanovo', 6, 1,0,0,false,6,false,1),
            ('Skopje', 1, 1,0,0,false,7,false,2),
            ('SRB', 1, 1,0,0,false,8,false,3),
            ('Cleveland', 1, 1,0,0,false,1,false,2);
            
        RAISE NOTICE '8 rows successfully inserted into nba.team.';
    ELSE
        RAISE NOTICE 'Table nba.team does not exist. No action taken.';
    END IF;
END $$;
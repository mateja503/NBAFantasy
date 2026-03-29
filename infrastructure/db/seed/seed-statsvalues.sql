DO $$
BEGIN
    -- Check if the table 'statsvalue' exists in the 'nba' schema
    IF EXISTS (
        SELECT FROM information_schema.tables 
        WHERE table_schema = 'nba' 
        AND table_name   = 'statsvalue'
    ) THEN
        INSERT INTO nba.statsvalue (
            pointsvalue, 
            assistsvalue, 
            reboundsvalue, 
            blocksvalue, 
            threepointsvaluemade, 
            threepointsvaluemissed, 
            turnoversvalue, 
            freethrowvaluemade, 
            freethrowvaluemissed, 
            fieldgoalvaluemade, 
            fieldgoalvaluemissed
        ) 
        VALUES (
            1.0,  -- pointsvalue
            1.5,  -- assistsvalue
            1.2,  -- reboundsvalue
            2.0,  -- blocksvalue
            3.0,  -- threepointsvaluemade
            -1.0, -- threepointsvaluemissed
            -2.0, -- turnoversvalue
            1.0,  -- freethrowvaluemade
            -1.0, -- freethrowvaluemissed
            2.0,  -- fieldgoalvaluemade
            -1.0  -- fieldgoalvaluemissed
        );
        
        RAISE NOTICE 'Row successfully inserted into nba.statsvalue.';
    ELSE
        RAISE NOTICE 'Table nba.statsvalue does not exist. No action taken.';
    END IF;
END $$;
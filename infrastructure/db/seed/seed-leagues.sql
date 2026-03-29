DO $$
BEGIN
    IF EXISTS (
        SELECT FROM information_schema.tables 
        WHERE table_schema = 'nba' 
        AND table_name   = 'league'
    ) THEN
        INSERT INTO nba.league (
            name, 
            commissioner, 
            seasonyear, 
            weeksforseason, 
            transactionlimit, 
            autostart, 
            typetransactionlimits, 
            typeleague, 
            draftstyle, 
            statsvalueid
        ) 
        VALUES ('National Basketball Association', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 1);
        
        RAISE NOTICE 'Row successfully inserted into nba.league.';
    ELSE
        RAISE NOTICE 'Table nba.league does not exist. No action taken.';
    END IF;
END $$;
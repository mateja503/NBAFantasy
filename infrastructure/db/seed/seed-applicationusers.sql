DO $$
BEGIN
    -- Check if the table exists in the 'nba' schema
    IF EXISTS (
        SELECT FROM information_schema.tables 
        WHERE table_schema = 'nba' 
          AND table_name   = 'applicationuser'
    ) THEN
        -- If it exists, execute the inserts
        INSERT INTO nba.applicationuser (username, password, email, xp, managerlevel) VALUES
        ('lebron_fan23', 'Test.123', 'kingj@example.com', 1500, 5),
        ('curry_cooks', 'Test.123', 'steph.fan@example.com', 450, 2),
        ('dunk_master', 'Test.123', 'dunkmaster@example.com', 3200, 10),
        ('court_vision', 'Test.123', 'pointgod@example.com', 0, 1),
        ('celtics_pride', 'Test.123', 'greenrun@example.com', 850, 3),
        ('buzzer_beater', 'Test.123', 'clutch@example.com', 2100, 7),
        ('sk_mzt', 'Test.123', 'mzt@example.com', 443, 4),
        ('czv_48', 'Test.123', 'czv@example.com', 117, 8);
        
        RAISE NOTICE 'Table nba.applicationuser exists. Seed data inserted successfully.';
    ELSE
        RAISE NOTICE 'Table nba.applicationuser does not exist. Skipping insertion.';
    END IF;
END $$;
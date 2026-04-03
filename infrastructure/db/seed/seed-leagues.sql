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
        VALUES 
            ('National Basketball Association', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 1),
            ('Fantasy League 01', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 2),
            ('Fantasy League 02', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 3),
            ('Fantasy League 03', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 4),
            ('Fantasy League 04', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 5),
            ('Fantasy League 05', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 6),
            ('Fantasy League 06', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 7),
            ('Fantasy League 07', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 8),
            ('Fantasy League 08', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 9),
            ('Fantasy League 09', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 10),
            ('Fantasy League 10', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 11),
            ('Fantasy League 11', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 12),
            ('Fantasy League 12', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 13),
            ('Fantasy League 13', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 14),
            ('Fantasy League 14', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 15),
            ('Fantasy League 15', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 16),
            ('Fantasy League 16', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 17),
            ('Fantasy League 17', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 18),
            ('Fantasy League 18', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 19),
            ('Fantasy League 19', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 20),
            ('Fantasy League 20', 1, '2026/2027', 24, 10, TRUE, 1, 1, 1, 21);
            
        RAISE NOTICE '21 rows successfully inserted into nba.league.';
    ELSE
        RAISE NOTICE 'Table nba.league does not exist. No action taken.';
    END IF;
END $$;
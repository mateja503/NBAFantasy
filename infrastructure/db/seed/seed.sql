INSERT INTO nba.League (name, tscreated, usercreated)
VALUES ('Eurolige', CURRENT_TIMESTAMP, 'admin')
RETURNING leagueid;

INSERT INTO nba.League (name, tscreated, usercreated)
VALUES ('NBA', CURRENT_TIMESTAMP, 'admin')
RETURNING leagueid;


INSERT INTO nba.Team (name, tscreated, usercreated)
VALUES ('Monaco', CURRENT_TIMESTAMP, 'admin')
RETURNING teamid;

INSERT INTO nba.Player (name, teamid, leagueid, tscreated, usercreated)
VALUES ('Mike James', 1, 1, CURRENT_TIMESTAMP, 'admin');
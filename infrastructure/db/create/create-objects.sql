CREATE SCHEMA IF NOT EXISTS nba;

-- 1. League Table
CREATE TABLE nba.league (
    leagueid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    tscreated TIMESTAMP WITH TIME ZONE NOT NULL,
    tsupdated TIMESTAMP WITH TIME ZONE,
    usercreated VARCHAR(255) NOT NULL,
    userupdated VARCHAR(255)
);

-- 2. Team Table
CREATE TABLE nba.team (
    teamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    tscreated TIMESTAMP WITH TIME ZONE NOT NULL,
    tsupdated TIMESTAMP WITH TIME ZONE,
    usercreated VARCHAR(255) NOT NULL,
    userupdated VARCHAR(255)
);

-- 4. User Table
CREATE TABLE nba.user (
    userid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
	password VARCHAR(255) NOT NULL,
    tscreated TIMESTAMP WITH TIME ZONE NOT NULL,
    tsupdated TIMESTAMP WITH TIME ZONE,
    usercreated VARCHAR(255) NOT NULL,
    userupdated VARCHAR(255)
);

-- 5. RangList Table
CREATE TABLE nba.rangList (
    ranglistid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
	totalpoints BIGINT NOT NULL,
    tscreated TIMESTAMP WITH TIME ZONE NOT NULL,
    tsupdated TIMESTAMP WITH TIME ZONE,
    usercreated VARCHAR(255) NOT NULL,
    userupdated VARCHAR(255)
);

-- 3. Player Table 
-- Constraints: 1 team per league, no duplicates for player/team/league combo
CREATE TABLE nba.player (
    playerid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    teamid BIGINT REFERENCES nba.Team(teamid),
    leagueid BIGINT REFERENCES nba.League(leagueid),
    tscreated TIMESTAMP WITH TIME ZONE NOT NULL,
    tsupdated TIMESTAMP WITH TIME ZONE,
    usercreated VARCHAR(255) NOT NULL,
    userupdated VARCHAR(255),
    -- Ensures a player is unique within a specific team and league
    CONSTRAINT unique_player_session UNIQUE(name, teamid, leagueid)
);

CREATE TABLE nba.leagueTeam (
    leagueteamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    league_id BIGINT REFERENCES nba.League(leagueid),
    team_id BIGINT REFERENCES nba.Team(teamid),
    UNIQUE(league_id, team_id)
);

-- "rang list can have many teams and many users"
CREATE TABLE nba.rangListTeam (
    rlteamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ranglist_id BIGINT REFERENCES nba.RangList(ranglistid),
    team_id BIGINT REFERENCES nba.Team(teamid),
    UNIQUE(ranglist_id, team_id)
);

CREATE TABLE nba.rangListUser (
    rluserid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ranglist_id BIGINT REFERENCES nba.RangList(ranglistid),
    user_id BIGINT REFERENCES nba.User(userid),
    UNIQUE(ranglist_id, user_id)
);

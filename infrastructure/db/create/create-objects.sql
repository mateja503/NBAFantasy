DROP SCHEMA IF EXISTS nba CASCADE;
CREATE SCHEMA nba;

CREATE TABLE nba.playermemento (
    playermemontoid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    playersteam VARCHAR(100),
    points BIGINT DEFAULT 0,
    assists BIGINT DEFAULT 0,
    rebounds BIGINT DEFAULT 0,
    blocks BIGINT DEFAULT 0,
    steals BIGINT DEFAULT 0,
    threepointers BIGINT DEFAULT 0,
    turnovers BIGINT DEFAULT 0,
    freethrowperc DOUBLE PRECISION,
    fieldgoalperc DOUBLE PRECISION,
    tscreated TIMESTAMP WITH TIME ZONE
);

CREATE TABLE nba.player (
    playerid BIGINT PRIMARY KEY,
    surname VARCHAR(255) NOT NULL,
    name VARCHAR(255) NOT NULL,
    irlteamname VARCHAR(100),
	irlteamid BIGINT,
    points BIGINT DEFAULT 0,
    assists BIGINT DEFAULT 0,
    rebounds BIGINT DEFAULT 0,
    blocks BIGINT DEFAULT 0,
    steals BIGINT DEFAULT 0,
    threepointers BIGINT DEFAULT 0,
    turnovers BIGINT DEFAULT 0,
    freethrowperc DOUBLE PRECISION,
    fieldgoalperc DOUBLE PRECISION,
    isdrop BOOLEAN DEFAULT FALSE,
    isfreeagent BOOLEAN DEFAULT TRUE,
    allowdrop BOOLEAN DEFAULT FALSE,
    islock BOOLEAN DEFAULT FALSE,
    tsupdated TIMESTAMP WITH TIME ZONE,
    tscreated TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    playerposition INTEGER,
    rosterrole INTEGER,
    gameready INTEGER,
    playermemontoid BIGINT UNIQUE,
    FOREIGN KEY (playermemontoid) REFERENCES nba.playermemento(playermemontoid)
);

CREATE TABLE nba.team (
    teamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    seed INTEGER,
    waiverpriority INTEGER,
    lastweekpoints DOUBLE PRECISION DEFAULT 0,
    categoryleaguepoints DOUBLE PRECISION DEFAULT 0,
    islock BOOLEAN DEFAULT FALSE
);

CREATE TABLE nba.teamplayer (
    teamplayerid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    playerid BIGINT NOT NULL,
    teamid BIGINT NOT NULL,
    CONSTRAINT fk_teamplayer_player FOREIGN KEY (playerid) REFERENCES nba.player(playerid),
    CONSTRAINT fk_teamplayer_team FOREIGN KEY (teamid) REFERENCES nba.team(teamid)
);

CREATE TABLE nba.statsvalue (
    statsvalueid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    pointsvalue DOUBLE PRECISION NOT NULL,
    assistsvalue DOUBLE PRECISION NOT NULL,
    reboundsvalue DOUBLE PRECISION NOT NULL,
    blocksvalue DOUBLE PRECISION NOT NULL,
    threepointsvalue DOUBLE PRECISION NOT NULL,
    turnoversvalue DOUBLE PRECISION NOT NULL,
    freethrowpervalue DOUBLE PRECISION NOT NULL,
    fieldgoalpercvalue DOUBLE PRECISION NOT NULL
);

CREATE TABLE nba.league (
    leagueid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    commissioner BIGINT NOT NULL,
    seasonyear VARCHAR(255) NOT NULL,
    weeksforseason INTEGER,
    transactionlimit INTEGER,
    autostart BOOLEAN,
    typetransactionlimits INTEGER,
    typeleague INTEGER,
    draftstyle INTEGER,
    statsvalueid BIGINT UNIQUE,
    FOREIGN KEY (statsvalueid) REFERENCES nba.statsvalue(statsvalueid)
);

CREATE TABLE nba.leagueteam (
    leagueteamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    teamid BIGINT NOT NULL,
    leagueid BIGINT NOT NULL,
    CONSTRAINT fk_leagueteam_team FOREIGN KEY (teamid) REFERENCES nba.team(teamid),
    CONSTRAINT fk_leagueteam_league FOREIGN KEY (leagueid) REFERENCES nba.league(leagueid)
);

CREATE TABLE nba.leagueplayer (
    leagueplayerid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    playerid BIGINT NOT NULL,
    leagueid BIGINT NOT NULL,
    CONSTRAINT fk_leagueplayer_player FOREIGN KEY (playerid) REFERENCES nba.player(playerid),
    CONSTRAINT fk_leagueplayer_league FOREIGN KEY (leagueid) REFERENCES nba.league(leagueid)
);

CREATE TABLE nba.playoff (
    playoffid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    totalrounds INTEGER NOT NULL DEFAULT 4,
    leagueid BIGINT,
    CONSTRAINT fk_playoff_league FOREIGN KEY (leagueid) REFERENCES nba.league(leagueid)
);

CREATE TABLE nba.playoffbracket (
    playoffbracketid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    playoffround INTEGER NOT NULL DEFAULT 1,
    team1 BIGINT NOT NULL,
    team2 BIGINT NOT NULL,
    playoffid BIGINT,
    CONSTRAINT fk_bracket_playoff FOREIGN KEY (playoffid) REFERENCES nba.playoff(playoffid)
);

CREATE TABLE nba.transactions (
    transactionid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    tscreated TIMESTAMP WITH TIME ZONE,
    typetransaction INTEGER,
    transactionstatus INTEGER
);

CREATE TABLE nba.transactionleague (
    transactionleagueid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    transactionid BIGINT NOT NULL,
    leagueid BIGINT NOT NULL,
    CONSTRAINT fk_trans_id FOREIGN KEY (transactionid) REFERENCES nba.transactions(transactionid),
    CONSTRAINT fk_trans_league FOREIGN KEY (leagueid) REFERENCES nba.league(leagueid)
);

CREATE TABLE nba.applicationuser (
    userid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(255),
    password VARCHAR(255),
    email VARCHAR(255),
    xp BIGINT DEFAULT 0,
    managerlevel INTEGER
);

CREATE TABLE nba.userteam (
    userteamid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    teamid BIGINT NOT NULL,
    userid BIGINT NOT NULL,
    CONSTRAINT fk_userteam_team FOREIGN KEY (teamid) REFERENCES nba.team(teamid),
    CONSTRAINT fk_userteam_user FOREIGN KEY (userid) REFERENCES nba.applicationuser(userid)
);

CREATE TABLE nba.userleague (
    userleagueid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    userid BIGINT NOT NULL,
    leagueid BIGINT NOT NULL,
    CONSTRAINT fk_userleague_user FOREIGN KEY (userid) REFERENCES nba.applicationuser(userid),
    CONSTRAINT fk_userleague_league FOREIGN KEY (leagueid) REFERENCES nba.league(leagueid)
);

CREATE TABLE nba.trophie (
    trophieid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    xp BIGINT DEFAULT 25,
    typetrophie VARCHAR(255)
);

CREATE TABLE nba.usertrophie (
    usertrophieid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    userid BIGINT NOT NULL,
    trophieid BIGINT NOT NULL,
    CONSTRAINT fk_usertrophie_user FOREIGN KEY (userid) REFERENCES nba.applicationuser(userid),
    CONSTRAINT fk_usertrophie_trophie FOREIGN KEY (trophieid) REFERENCES nba.trophie(trophieid)
);
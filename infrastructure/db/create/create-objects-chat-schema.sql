DROP SCHEMA IF EXISTS chat CASCADE;
CREATE SCHEMA chat;

-- 1. Rooms/Conversations
CREATE TABLE chat.rooms (
    roomid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(100),
    roomtype INT DEFAULT 0, -- 0 for Public, 1 for Private League, etc.
    tscreated TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- 2. Participants (The "Link" between Users and Rooms)
CREATE TABLE chat.conversationparticipants (
	conversationid BIGINT GENERATED ALWAYS AS IDENTITY,
    roomid BIGINT NOT NULL,
    userid BIGINT NOT NULL, -- Ensure this matches your User table type
    joinedat TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (roomid, userid), -- Prevents a user from joining the same room twice
    CONSTRAINT fk_room FOREIGN KEY (roomid) REFERENCES chat.rooms(roomid) ON DELETE CASCADE,
    CONSTRAINT fk_user FOREIGN KEY (userid) REFERENCES nba.applicationuser(userid)
);

-- 3. Messages
CREATE TABLE chat.messages (
    messageid BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    content TEXT,
    tscreated TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    issystemmessage BOOLEAN NOT NULL DEFAULT FALSE,
    roomid BIGINT NOT NULL,
    senderid BIGINT, -- Nullable to allow for System/Server messages
    CONSTRAINT fk_message_room FOREIGN KEY (roomid) REFERENCES chat.rooms(roomid) ON DELETE CASCADE,
    CONSTRAINT fk_message_sender FOREIGN KEY (senderid) REFERENCES nba.applicationuser(userid)
);

-- 4. Essential Indexes for Speed (Critical for Draft Performance)
CREATE INDEX idx_messages_roomid_ts ON chat.messages(roomid, tscreated DESC);
CREATE INDEX idx_participants_userid ON chat.conversationparticipants(userid);
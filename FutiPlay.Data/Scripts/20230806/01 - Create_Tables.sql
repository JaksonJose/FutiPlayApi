
/* Drops must be in the right sequence due the foreign keys */
DROP TABLE IF EXISTS dbo.PlayerStatistic
DROP TABLE IF EXISTS dbo.Match
DROP TABLE IF EXISTS dbo.Player
DROP TABLE IF EXISTS dbo.MatchStatistic
DROP TABLE IF EXISTS dbo.Team
DROP TABLE IF EXISTS dbo.Tournament

CREATE TABLE Tournament (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(255),
	StartDate DATETIME2,
	EndDate DATETIME2,
	Description VARCHAR(255),
	Status VARCHAR(255),
);

CREATE TABLE Team (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name VARCHAR(max),
	LogoUrl VARCHAR(255),
);

CREATE TABLE MatchStatistic(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,	
	Fouls INT,
	YellowCards INT,
	RedCards INT,
	ShotsOnGoal INT,
	Corners INT,
);

CREATE TABLE Match (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	GameDataTime datetime2,
	HomeTeamId INT FOREIGN KEY REFERENCES Team(id),
	AwayTeamId INT FOREIGN KEY REFERENCES Team(id),
	HomeTeamScore INT,
	AwayTeamScore INT,
	LocationMatch VARCHAR(255),
	StatusMatch VARCHAR(255),
	TournamentId INT FOREIGN KEY REFERENCES Tournament(Id),
	StatisticId INT FOREIGN KEY REFERENCES MatchStatistic(Id),
);

CREATE TABLE Player (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	NamePlayer VARCHAR(255),
	Position VARCHAR(255),
	TeamId INT FOREIGN KEY REFERENCES Team(Id)
);

CREATE TABLE PlayerStatistic(
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	MatchId INT FOREIGN KEY REFERENCES Match(Id),
	PlayerId VARCHAR(40) FOREIGN KEY REFERENCES Player(Id),
	TotalGoals VARCHAR(255),
	YellowCards INT,
	RedCards INT,
	ShotsOnGoal INT,
);
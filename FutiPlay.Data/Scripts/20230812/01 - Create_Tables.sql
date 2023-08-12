
/* Drops must be in the right sequence due the foreign keys */
DROP TABLE IF EXISTS dbo.Match
DROP TABLE IF EXISTS dbo.TeamMember
DROP TABLE IF EXISTS dbo.Team
DROP TABLE IF EXISTS dbo.Tournament

CREATE TABLE Tournament (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name NVARCHAR(255),
	StartDate DATETIME2,
	EndDate DATETIME2,
	Description NVARCHAR(255),
	Status VARCHAR(255),
	UpdatedAt DateTimeOffset,	
	UpdatedBY NVARCHAR(450),
	CreatedAt DateTimeOffset,
	CreatedBy NVARCHAR(450)	
);

CREATE TABLE Team (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Name NVARCHAR(max),
	ImageUrl NVARCHAR(255),
	Description NVARCHAR(MAX),
	Active BIT,
	UpdatedAt DateTimeOffset,	
	UpdatedBY NVARCHAR(450),
	CreatedAt DateTimeOffset,
	CreatedBy NVARCHAR(450)	
);

CREATE TABLE Match (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TournamentId INT FOREIGN KEY REFERENCES Tournament(Id),
	HomeTeamId INT FOREIGN KEY REFERENCES Team(id),
	AwayTeamId INT FOREIGN KEY REFERENCES Team(id),	
	GameDataTime datetime2,
	HomeTeamScore INT,
	AwayTeamScore INT,
	LocationMatch VARCHAR(255),
	Status VARCHAR(255),
	UpdatedAt DateTimeOffset,	
	UpdatedBY NVARCHAR(450),
	CreatedAt DateTimeOffset,
	CreatedBy NVARCHAR(450)	
);

CREATE TABLE TeamMember (
	Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserId NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id),
	TeamId INT FOREIGN KEY REFERENCES Team(Id),
	RoleId NVARCHAR(450) FOREIGN KEY REFERENCES AspNetRoles(Id),
	Positon VARCHAR,
	UpdatedAt DateTimeOffset,	
	UpdatedBY NVARCHAR(450),
	CreatedAt DateTimeOffset,
	CreatedBy NVARCHAR(450)	
);
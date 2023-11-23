-- [flight-database].dbo.UserData definition

-- Drop table

-- DROP TABLE [flight-database].dbo.UserData;

CREATE TABLE [flight-database].dbo.UserData (
	UserId int NOT NULL,
	UserName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Password nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Role] nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PilotID int DEFAULT 0 NOT NULL,
	UserEmail int DEFAULT 0 NOT NULL,
	CONSTRAINT PK_UserData PRIMARY KEY (UserId)
);
-- [flight-database].dbo.PilotProfile definition

-- Drop table

-- DROP TABLE [flight-database].dbo.PilotProfile;

CREATE TABLE [flight-database].dbo.PilotProfile (
	PilotId int IDENTITY(1,1) NOT NULL,
	UserProfileID int DEFAULT 0 NOT NULL,
	PilotRating nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	PilotProfileName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_PilotProfile PRIMARY KEY (PilotId)
);


-- [flight-database].dbo.PilotProfile foreign keys

ALTER TABLE [flight-database].dbo.PilotProfile ADD CONSTRAINT FK__PilotProf__UserP__2EDAF651 FOREIGN KEY (UserProfileID) REFERENCES [flight-database].dbo.UserData(UserId);
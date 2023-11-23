CREATE TABLE [flight-database].dbo.Airliner (
	Id int NOT NULL,
	AirlinerName nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AirlinerCallSign nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_Airliner PRIMARY KEY (Id)
);
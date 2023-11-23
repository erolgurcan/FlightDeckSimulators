-- [flight-database].dbo.FlightInfo definition

-- Drop table

-- DROP TABLE [flight-database].dbo.FlightInfo;

CREATE TABLE [flight-database].dbo.FlightInfo (
	id int IDENTITY(1,1) NOT NULL,
	FlightCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	Airliner int NOT NULL,
	isActive bit NOT NULL,
	DepartedDate datetime2 NOT NULL,
	ArivalAirportCode nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	DepartureAirportCode nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	AirplaneModel nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT N'' NOT NULL,
	TailNumber nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS DEFAULT N'' NOT NULL,
	PilotID int DEFAULT 0 NOT NULL,
	CONSTRAINT PK_FlightInfo PRIMARY KEY (FlightCode)
);


-- [flight-database].dbo.FlightInfo foreign keys

ALTER TABLE [flight-database].dbo.FlightInfo ADD CONSTRAINT FK__FlightInf__Airli__2B0A656D FOREIGN KEY (Airliner) REFERENCES [flight-database].dbo.Airliner(Id);
ALTER TABLE [flight-database].dbo.FlightInfo ADD CONSTRAINT FK__FlightInf__Pilot__2BFE89A6 FOREIGN KEY (PilotID) REFERENCES [flight-database].dbo.PilotProfile(PilotId);
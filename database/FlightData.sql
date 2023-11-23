-- [flight-database].dbo.FlightData definition

-- Drop table

-- DROP TABLE [flight-database].dbo.FlightData;

CREATE TABLE [flight-database].dbo.FlightData (
	Id int NOT NULL,
	LoggingTime datetime2 NOT NULL,
	TrueAirSpeed float NOT NULL,
	GroundSpeed float NOT NULL,
	Latitude float NOT NULL,
	Longitude float NOT NULL,
	Altitude float NOT NULL,
	Throttle float NOT NULL,
	Heading float DEFAULT 0.0000000000000000e+000 NOT NULL,
	AOA float DEFAULT 0.0000000000000000e+000 NOT NULL,
	Throttle1 float DEFAULT 0.0000000000000000e+000 NOT NULL,
	Throttle2 float DEFAULT 0.0000000000000000e+000 NOT NULL,
	LandingGear int DEFAULT 0 NOT NULL,
	OutsideTemperature float DEFAULT 0.0000000000000000e+000 NOT NULL,
	TotalFuel float DEFAULT 0.0000000000000000e+000 NOT NULL,
	FlightCode varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_FlightData PRIMARY KEY (Id)
);


-- [flight-database].dbo.FlightData foreign keys

ALTER TABLE [flight-database].dbo.FlightData ADD CONSTRAINT FK__FlightDat__Fligh__3B40CD36 FOREIGN KEY (FlightCode) REFERENCES [flight-database].dbo.FlightInfo(FlightCode);
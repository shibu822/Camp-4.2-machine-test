/*  CREATE & USE DATABASE */
CREATE DATABASE FlyWithMeDB;
GO
USE FlyWithMeDB;
GO


/*  ADMIN TABLE */
CREATE TABLE Admin (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(50) NOT NULL
);
GO


/*COUNTRY TABLE*/
CREATE TABLE Country (
    CountryId INT IDENTITY(1,1) PRIMARY KEY,
    CountryName VARCHAR(50) NOT NULL UNIQUE
);
GO


/* CITY TABLE*/
CREATE TABLE City (
    CityId INT IDENTITY(1,1) PRIMARY KEY,
    CityName VARCHAR(50) NOT NULL,
    CountryId INT NOT NULL,
    CONSTRAINT FK_City_Country
        FOREIGN KEY (CountryId)
        REFERENCES Country(CountryId)
);
GO


/*AIRPORT TABLE */
CREATE TABLE Airport (
    AirportId INT IDENTITY(1,1) PRIMARY KEY,
    AirportCode VARCHAR(10) NOT NULL UNIQUE,
    AirportName VARCHAR(100) NOT NULL,
    CityId INT NOT NULL,
    CONSTRAINT FK_Airport_City
        FOREIGN KEY (CityId)
        REFERENCES City(CityId)
);
GO


/* FLIGHT TABLE */
CREATE TABLE Flight (
    FlightId INT IDENTITY(1,1) PRIMARY KEY,
    FlightName VARCHAR(100) NOT NULL,
    FlightCode VARCHAR(20) NOT NULL UNIQUE
);
GO


/*FLIGHT DETAILS TABLE */
CREATE TABLE FlightDetails (
    FlightDetailId INT IDENTITY(1,1) PRIMARY KEY,
    FlightId INT NOT NULL,
    DepAirportId INT NOT NULL,
    ArrAirportId INT NOT NULL,
    DepDate DATE NOT NULL,
    DepTime TIME NOT NULL,
    ArrDate DATE NOT NULL,
    ArrTime TIME NOT NULL,
    AdminId INT NOT NULL,

    CONSTRAINT FK_FlightDetails_Flight
        FOREIGN KEY (FlightId)
        REFERENCES Flight(FlightId),

    CONSTRAINT FK_FlightDetails_DepAirport
        FOREIGN KEY (DepAirportId)
        REFERENCES Airport(AirportId),

    CONSTRAINT FK_FlightDetails_ArrAirport
        FOREIGN KEY (ArrAirportId)
        REFERENCES Airport(AirportId),

    CONSTRAINT FK_FlightDetails_Admin
        FOREIGN KEY (AdminId)
        REFERENCES Admin(AdminId)
);
GO


/* CHECK CONSTRAINT */
ALTER TABLE FlightDetails
ADD CONSTRAINT CHK_Arrival_After_Departure
CHECK (
    (ArrDate > DepDate)
    OR
    (ArrDate = DepDate AND ArrTime > DepTime)
);
GO


/*INSERT DATA */
INSERT INTO Country (CountryName)
VALUES ('India');

INSERT INTO City (CityName, CountryId)
VALUES ('Chennai', 1),
       ('Delhi', 1);

INSERT INTO Airport (AirportCode, AirportName, CityId)
VALUES ('MAA', 'Chennai International Airport', 1),
       ('DEL', 'Indira Gandhi International Airport', 2);

INSERT INTO Flight (FlightName, FlightCode)
VALUES ('AirIndia Express', 'F101');

INSERT INTO Admin (Username, Password)
VALUES ('admin', 'admin123');
GO


/* INSERT FLIGHT DETAILS*/
INSERT INTO FlightDetails
(FlightId, DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime, AdminId)
VALUES
(1, 1, 2, '2026-02-01', '10:00', '2026-02-01', '12:30', 1);
GO


/* VIEW FLIGHT DETAILS (RAW)*/
SELECT * FROM FlightDetails;
GO


/*VIEW FLIGHT DETAILS (JOINED)*/
SELECT 
    fd.FlightDetailId,
    f.FlightName,
    f.FlightCode,
    a1.AirportName AS DepartureAirport,
    a2.AirportName AS ArrivalAirport,
    fd.DepDate,
    fd.DepTime,
    fd.ArrDate,
    fd.ArrTime
FROM FlightDetails fd
JOIN Flight f ON fd.FlightId = f.FlightId
JOIN Airport a1 ON fd.DepAirportId = a1.AirportId
JOIN Airport a2 ON fd.ArrAirportId = a2.AirportId;
GO


/* LOGIN TEST  */
DECLARE @Username VARCHAR(50) = 'admin';
DECLARE @Password VARCHAR(50) = 'admin123';

SELECT AdminId
FROM Admin
WHERE Username = @Username
  AND Password = @Password;
GO

SELECT * FROM Flight;

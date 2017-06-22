
ALTER TABLE dbo.TimeZoneXCountry
	ADD CONSTRAINT FK_TimeZoneXCountry_TimeZone FOREIGN KEY
	(
		TimeZoneId
	)
	REFERENCES dbo.TimeZone
	(
		TimeZoneId
	)
GO

ALTER TABLE dbo.TimeZoneXCountry
	ADD CONSTRAINT FK_TimeZoneXCountry_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO






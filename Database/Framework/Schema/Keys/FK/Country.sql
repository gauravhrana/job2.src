ALTER TABLE dbo.Country
	ADD CONSTRAINT FK_Country_TimeZone FOREIGN KEY
	(
		TimeZoneId
	)
	REFERENCES dbo.TimeZone
	(
		TimeZoneId 
	)
GO
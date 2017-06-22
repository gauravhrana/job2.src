
ALTER TABLE dbo.HolidayXCountry
	ADD CONSTRAINT FK_HolidayXCountry_Holiday FOREIGN KEY
	(
		HolidayId
	)
	REFERENCES dbo.Holiday
	(
		HolidayId
	)
GO


ALTER TABLE dbo.HolidayXCountry
	ADD CONSTRAINT FK_HolidayXCountry_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO





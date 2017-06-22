
ALTER TABLE dbo.City
	ADD CONSTRAINT FK_City_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








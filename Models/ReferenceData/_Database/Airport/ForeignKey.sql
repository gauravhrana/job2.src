
ALTER TABLE dbo.Airport
	ADD CONSTRAINT FK_Airport_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








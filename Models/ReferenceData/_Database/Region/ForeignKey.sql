
ALTER TABLE dbo.Region
	ADD CONSTRAINT FK_Region_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








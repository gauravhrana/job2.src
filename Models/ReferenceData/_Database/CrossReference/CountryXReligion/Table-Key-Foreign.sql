
ALTER TABLE dbo.CountryXReligion
	ADD CONSTRAINT FK_CountryXReligion_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO

ALTER TABLE dbo.CountryXReligion
	ADD CONSTRAINT FK_CountryXReligion_Religion FOREIGN KEY
	(
		ReligionId
	)
	REFERENCES dbo.Religion
	(
		ReligionId
	)
GO






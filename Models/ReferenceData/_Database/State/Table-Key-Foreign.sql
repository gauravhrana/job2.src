
ALTER TABLE dbo.State
	ADD CONSTRAINT FK_State_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








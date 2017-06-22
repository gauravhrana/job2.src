
ALTER TABLE dbo.TrainStation
	ADD CONSTRAINT FK_TrainStation_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








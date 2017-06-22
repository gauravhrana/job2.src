
ALTER TABLE dbo.Address
	ADD CONSTRAINT FK_Address_City FOREIGN KEY
	(
		CityId
	)
	REFERENCES dbo.City
	(
		CityId
	)
GO


ALTER TABLE dbo.Address
	ADD CONSTRAINT FK_Address_State FOREIGN KEY
	(
		StateId
	)
	REFERENCES dbo.State
	(
		StateId
	)
GO


ALTER TABLE dbo.Address
	ADD CONSTRAINT FK_Address_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO








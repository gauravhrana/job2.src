
ALTER TABLE dbo.Province
	ADD CONSTRAINT FK_Province_Country FOREIGN KEY
	(
		CountryId
	)
	REFERENCES dbo.Country
	(
		CountryId
	)
GO


ALTER TABLE dbo.Province
	ADD CONSTRAINT FK_Province_ProvinceType FOREIGN KEY
	(
		ProvinceTypeId
	)
	REFERENCES dbo.ProvinceType
	(
		ProvinceTypeId
	)
GO








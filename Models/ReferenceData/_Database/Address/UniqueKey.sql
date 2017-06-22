IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Address')
	AND		name	= N'UQ_Address_ApplicationId_CityId_StateId_CountryId'
)
BEGIN
	PRINT	'Dropping UQ_Address_ApplicationId_CityId_StateId_CountryId'
	ALTER	TABLE dbo.Address
	DROP	CONSTRAINT	UQ_Address_ApplicationId_CityId_StateId_CountryId
END
GO

ALTER TABLE dbo.Address
ADD CONSTRAINT UQ_Address_ApplicationId_CityId_StateId_CountryId UNIQUE NONCLUSTERED
(
	ApplicationId, CityId, StateId, CountryId
)
GO

IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Province')
	AND		name	= N'UQ_Province_ApplicationId_CountryId_ProvinceTypeId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Province_ApplicationId_CountryId_ProvinceTypeId_Name'
	ALTER	TABLE dbo.Province
	DROP	CONSTRAINT	UQ_Province_ApplicationId_CountryId_ProvinceTypeId_Name
END
GO

ALTER TABLE dbo.Province
ADD CONSTRAINT UQ_Province_ApplicationId_CountryId_ProvinceTypeId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, CountryId, ProvinceTypeId, Name
)
GO

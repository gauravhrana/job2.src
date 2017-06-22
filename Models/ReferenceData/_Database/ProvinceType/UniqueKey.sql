IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ProvinceType')
	AND		name	= N'UQ_ProvinceType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ProvinceType_ApplicationId_Name'
	ALTER	TABLE dbo.ProvinceType
	DROP	CONSTRAINT	UQ_ProvinceType_ApplicationId_Name
END
GO

ALTER TABLE dbo.ProvinceType
ADD CONSTRAINT UQ_ProvinceType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

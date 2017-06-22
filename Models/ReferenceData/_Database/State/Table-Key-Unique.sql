IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].State')
	AND		name	= N'UQ_State_ApplicationId_CountryId_Name'
)
BEGIN
	PRINT	'Dropping UQ_State_ApplicationId_CountryId_Name'
	ALTER	TABLE dbo.State
	DROP	CONSTRAINT	UQ_State_ApplicationId_CountryId_Name
END
GO

ALTER TABLE dbo.State
ADD CONSTRAINT UQ_State_ApplicationId_CountryId_Name UNIQUE NONCLUSTERED
(
	ApplicationId, CountryId, Name
)
GO

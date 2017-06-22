IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[DevelopmentCategory]')
	AND		name	= N'UQ_DevelopmentCategory_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_DevelopmentCategory_Name_ApplicationId'
	ALTER	TABLE dbo.DevelopmentCategory
	DROP	CONSTRAINT	UQ_DevelopmentCategory_Name_ApplicationId
END
GO

ALTER TABLE dbo.DevelopmentCategory
ADD CONSTRAINT UQ_DevelopmentCategory_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name
	,	ApplicationId
)
GO

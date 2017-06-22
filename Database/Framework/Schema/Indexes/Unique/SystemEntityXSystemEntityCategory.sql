IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[SystemEntityXSystemEntityCategory]')
	AND		name	= N'UQ_SystemEntityXSystemEntityCategory_ApplicationId_SystemEntityId_SystemEntityCategoryId'
)
BEGIN
	PRINT	'Dropping UQ_SystemEntityXSystemEntityCategory_ApplicationId_SystemEntityId_SystemEntityCategoryId'
	ALTER	TABLE dbo.SystemEntityXSystemEntityCategory
	DROP	CONSTRAINT	UQ_SystemEntityXSystemEntityCategory_ApplicationId_SystemEntityId_SystemEntityCategoryId
END
GO

ALTER TABLE dbo.SystemEntityXSystemEntityCategory
ADD CONSTRAINT UQ_SystemEntityXSystemEntityCategory_ApplicationId_SystemEntityId_SystemEntityCategoryId UNIQUE NONCLUSTERED
(
		ApplicationId
	,	SystemEntityId
	,	SystemEntityCategoryId	
)
GO

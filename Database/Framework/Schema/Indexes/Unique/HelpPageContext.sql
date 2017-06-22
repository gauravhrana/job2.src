IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[HelpPageContext]')
	AND		name	= N'UQ_HelpPageContext_Name_ApplicationId'
)
BEGIN
	PRINT	'Dropping UQ_HelpPageContext_Name_ApplicationId'
	ALTER	TABLE dbo.HelpPageContext
	DROP	CONSTRAINT	UQ_HelpPageContext_Name_ApplicationId
END
GO

ALTER TABLE dbo.HelpPageContext
ADD CONSTRAINT UQ_HelpPageContext_Name_ApplicationId UNIQUE NONCLUSTERED
(
		Name	
	,	ApplicationId
)
GO

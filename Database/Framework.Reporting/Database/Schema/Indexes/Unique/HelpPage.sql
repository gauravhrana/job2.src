IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.HelpPage')
	AND		name	= N'UQ_HelpPage_Name_ApplicationId_SystemEntityTypeId_HelpPageContextId'
)
BEGIN
	PRINT	'Dropping UQ_HelpPage_Name_ApplicationId_SystemEntityTypeId_HelpPageContextId'
	ALTER TABLE dbo.HelpPage
		DROP CONSTRAINT	UQ_HelpPage_Name_ApplicationId_SystemEntityTypeId_HelpPageContextId
END
GO

ALTER TABLE dbo.HelpPage
	ADD CONSTRAINT UQ_HelpPage_Name_ApplicationId_SystemEntityTypeId_HelpPageContextId_SystemEntityTypeId_HelpPageContextId UNIQUE NONCLUSTERED
	(
			Name	
		,	ApplicationId
		,	SystemEntityTypeId
		,	HelpPageContextId
	)
GO

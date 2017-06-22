IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountAdministrator')
	AND		name	= N'UQ_AccountAdministrator_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountAdministrator_ApplicationId_Name'
	ALTER	TABLE dbo.AccountAdministrator
	DROP	CONSTRAINT	UQ_AccountAdministrator_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountAdministrator
ADD CONSTRAINT UQ_AccountAdministrator_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].HelpLine')
	AND		name	= N'UQ_HelpLine_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_HelpLine_ApplicationId_Name'
	ALTER	TABLE dbo.HelpLine
	DROP	CONSTRAINT	UQ_HelpLine_ApplicationId_Name
END
GO

ALTER TABLE dbo.HelpLine
ADD CONSTRAINT UQ_HelpLine_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

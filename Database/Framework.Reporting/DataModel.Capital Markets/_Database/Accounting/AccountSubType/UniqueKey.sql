IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountSubType')
	AND		name	= N'UQ_AccountSubType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountSubType_ApplicationId_Name'
	ALTER	TABLE dbo.AccountSubType
	DROP	CONSTRAINT	UQ_AccountSubType_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountSubType
ADD CONSTRAINT UQ_AccountSubType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

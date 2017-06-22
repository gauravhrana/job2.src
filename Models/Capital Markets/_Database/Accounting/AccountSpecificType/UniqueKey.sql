IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].AccountSpecificType')
	AND		name	= N'UQ_AccountSpecificType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_AccountSpecificType_ApplicationId_Name'
	ALTER	TABLE dbo.AccountSpecificType
	DROP	CONSTRAINT	UQ_AccountSpecificType_ApplicationId_Name
END
GO

ALTER TABLE dbo.AccountSpecificType
ADD CONSTRAINT UQ_AccountSpecificType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SexType')
	AND		name	= N'UQ_SexType_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SexType_ApplicationId_Name'
	ALTER	TABLE dbo.SexType
	DROP	CONSTRAINT	UQ_SexType_ApplicationId_Name
END
GO

ALTER TABLE dbo.SexType
ADD CONSTRAINT UQ_SexType_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Right')
	AND		name	= N'UQ_Right_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Right_ApplicationId_Name'
	ALTER	TABLE dbo.Right
	DROP	CONSTRAINT	UQ_Right_ApplicationId_Name
END
GO

ALTER TABLE dbo.Right
ADD CONSTRAINT UQ_Right_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

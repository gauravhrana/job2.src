IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Comment')
	AND		name	= N'UQ_Comment_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Comment_ApplicationId_Name'
	ALTER	TABLE dbo.Comment
	DROP	CONSTRAINT	UQ_Comment_ApplicationId_Name
END
GO

ALTER TABLE dbo.Comment
ADD CONSTRAINT UQ_Comment_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

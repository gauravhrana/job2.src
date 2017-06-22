IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].SubjectMatter')
	AND		name	= N'UQ_SubjectMatter_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_SubjectMatter_ApplicationId_Name'
	ALTER	TABLE dbo.SubjectMatter
	DROP	CONSTRAINT	UQ_SubjectMatter_ApplicationId_Name
END
GO

ALTER TABLE dbo.SubjectMatter
ADD CONSTRAINT UQ_SubjectMatter_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

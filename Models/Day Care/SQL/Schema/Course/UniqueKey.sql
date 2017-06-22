IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Course')
	AND		name	= N'UQ_Course_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Course_ApplicationId_Name'
	ALTER	TABLE dbo.Course
	DROP	CONSTRAINT	UQ_Course_ApplicationId_Name
END
GO

ALTER TABLE dbo.Course
ADD CONSTRAINT UQ_Course_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

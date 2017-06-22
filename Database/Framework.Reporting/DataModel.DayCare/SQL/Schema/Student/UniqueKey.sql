IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Student')
	AND		name	= N'UQ_Student_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Student_ApplicationId_Name'
	ALTER	TABLE dbo.Student
	DROP	CONSTRAINT	UQ_Student_ApplicationId_Name
END
GO

ALTER TABLE dbo.Student
ADD CONSTRAINT UQ_Student_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

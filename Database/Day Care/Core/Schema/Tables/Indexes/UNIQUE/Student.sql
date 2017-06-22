IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Student]')
	AND		name	= N'UQ_Student_ApplicationId_LastName_FirstName'
)
BEGIN
	PRINT	'Dropping UQ_Student_ApplicationId_LastName_FirstName'
	ALTER	TABLE dbo.Student
	DROP	CONSTRAINT	UQ_Student_ApplicationId_LastName_FirstName
END
GO

ALTER TABLE dbo.Student
ADD CONSTRAINT UQ_Student_ApplicationId_LastName_FirstName UNIQUE NONCLUSTERED
(
		ApplicationId
	,	LastName
	,	FirstName	
)
GO

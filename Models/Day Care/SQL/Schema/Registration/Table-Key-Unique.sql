IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Registration')
	AND		name	= N'UQ_Registration_ApplicationId_CourseId_StudentId'
)
BEGIN
	PRINT	'Dropping UQ_Registration_ApplicationId_CourseId_StudentId'
	ALTER	TABLE dbo.Registration
	DROP	CONSTRAINT	UQ_Registration_ApplicationId_CourseId_StudentId
END
GO

ALTER TABLE dbo.Registration
ADD CONSTRAINT UQ_Registration_ApplicationId_CourseId_StudentId UNIQUE NONCLUSTERED
(
	ApplicationId, CourseId, StudentId
)
GO

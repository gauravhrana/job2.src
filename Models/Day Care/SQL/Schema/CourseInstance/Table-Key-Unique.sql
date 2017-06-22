IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].CourseInstance')
	AND		name	= N'UQ_CourseInstance_ApplicationId_CourseId_DepartmentId_TeacherId'
)
BEGIN
	PRINT	'Dropping UQ_CourseInstance_ApplicationId_CourseId_DepartmentId_TeacherId'
	ALTER	TABLE dbo.CourseInstance
	DROP	CONSTRAINT	UQ_CourseInstance_ApplicationId_CourseId_DepartmentId_TeacherId
END
GO

ALTER TABLE dbo.CourseInstance
ADD CONSTRAINT UQ_CourseInstance_ApplicationId_CourseId_DepartmentId_TeacherId UNIQUE NONCLUSTERED
(
	ApplicationId, CourseId, DepartmentId, TeacherId
)
GO

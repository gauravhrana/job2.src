IF EXISTS 
(
	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].ClassInstance')
	AND		name	= N'UQ_ClassInstance_ApplicationId_CourseId_DepartmentId_TeacherId'
)
BEGIN
	PRINT	'Dropping UQ_ClassInstance_ApplicationId_CourseId_DepartmentId_TeacherId'
	ALTER	TABLE dbo.ClassInstance
	DROP	CONSTRAINT	UQ_ClassInstance_ApplicationId_CourseId_DepartmentId_TeacherId
END
GO

ALTER TABLE dbo.ClassInstance
ADD CONSTRAINT UQ_ClassInstance_ApplicationId_CourseId_DepartmentId_TeacherId UNIQUE NONCLUSTERED
(
	ApplicationId, CourseId, DepartmentId, TeacherId
)
GO

IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Teacher]')
	AND		name	= N'UQ_Teacher_ApplicationId_LastName_FirstName'
)
BEGIN
	PRINT	'Dropping UQ_Teacher_ApplicationId_LastName_FirstName'
	ALTER	TABLE dbo.Teacher
	DROP	CONSTRAINT	UQ_Teacher_ApplicationId_LastName_FirstName
END
GO

ALTER TABLE dbo.Teacher
ADD CONSTRAINT UQ_Teacher_ApplicationId_LastName_FirstName UNIQUE NONCLUSTERED
(
		ApplicationId
	,	LastName
	,	FirstName	
)
GO

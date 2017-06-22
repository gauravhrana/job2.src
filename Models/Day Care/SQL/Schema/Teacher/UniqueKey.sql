IF EXISTS 
(

	SELECT	*	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].Teacher')
	AND		name	= N'UQ_Teacher_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_Teacher_ApplicationId_Name'
	ALTER	TABLE dbo.Teacher
	DROP	CONSTRAINT	UQ_Teacher_ApplicationId_Name
END
GO

ALTER TABLE dbo.Teacher
ADD CONSTRAINT UQ_Teacher_ApplicationId_Name UNIQUE NONCLUSTERED
(
	ApplicationId
		,	Name	
	)
GO

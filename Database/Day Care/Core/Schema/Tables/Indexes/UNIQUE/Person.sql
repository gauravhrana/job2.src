IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[Person]')
	AND		name	= N'UQ_Person_ApplicationId_LastName_FirstName'
)
BEGIN
	PRINT	'Dropping UQ_Person_ApplicationId_LastName_FirstName'
	ALTER	TABLE dbo.Person
	DROP	CONSTRAINT	UQ_Person_ApplicationId_LastName_FirstName
END
GO

ALTER TABLE dbo.Person
ADD CONSTRAINT UQ_Person_ApplicationId_LastName_FirstName UNIQUE NONCLUSTERED
(
		ApplicationId
	,	LastName
	,	FirstName	
)
GO

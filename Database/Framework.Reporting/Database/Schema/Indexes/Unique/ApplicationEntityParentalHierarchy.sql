IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[ApplicationEntityParentalHierarchy]')
	AND		name	= N'UQ_ApplicationEntityParentalHierarchy_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_ApplicationEntityParentalHierarchy_ApplicationId_Name'
	ALTER	TABLE dbo.ApplicationEntityParentalHierarchy
	DROP	CONSTRAINT	UQ_ApplicationEntityParentalHierarchy_ApplicationId_Name
END
GO

ALTER TABLE dbo.ApplicationEntityParentalHierarchy
ADD CONSTRAINT UQ_ApplicationEntityParentalHierarchy_ApplicationId_Name UNIQUE NONCLUSTERED
(
		ApplicationId
	,	Name	
)
GO
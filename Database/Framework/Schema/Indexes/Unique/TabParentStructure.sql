IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.TabParentStructure')
	AND		name	= N'UQ_TabParentStructure_ApplicationId_Name'
)
BEGIN
	PRINT	'Dropping UQ_TabParentStructure_ApplicationId_Name'
	ALTER TABLE dbo.TabParentStructure
		DROP CONSTRAINT	UQ_TabParentStructure_ApplicationId_Name
END
GO

ALTER TABLE dbo.TabParentStructure
	ADD CONSTRAINT UQ_TabParentStructure_ApplicationId_Name UNIQUE NONCLUSTERED
	(
			ApplicationId	
		,	Name			
	)
GO

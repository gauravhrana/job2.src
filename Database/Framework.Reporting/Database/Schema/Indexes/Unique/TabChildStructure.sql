IF EXISTS
(
	SELECT *
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'dbo.TabChildStructure')
	AND		name	= N'UQ_TabChildStructure_ApplicationId_Name_TabParentStructureId_TabParentStructureId'
)
BEGIN
	PRINT	'Dropping UQ_TabChildStructure_ApplicationId_Name_TabParentStructureId'
	ALTER TABLE dbo.TabChildStructure
		DROP CONSTRAINT	UQ_TabChildStructure_ApplicationId_Name_TabParentStructureId
END
GO

ALTER TABLE dbo.TabChildStructure
	ADD CONSTRAINT UQ_TabChildStructure_ApplicationId_Name_TabParentStructureId UNIQUE NONCLUSTERED
	(
			ApplicationId	
		,	Name			
		,	TabParentStructureId
	)
GO

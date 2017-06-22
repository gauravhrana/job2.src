IF EXISTS
(
	SELECT	*
	FROM	dbo.sysindexes
	WHERE	id		= OBJECT_ID(N'[dbo].[TaskEntityType]')
	AND		name	= N'UQ_TaskEntityType_Name'
)
BEGIN
	PRINT	'Dropping UQ_TaskEntityType_Name'
	ALTER	TABLE dbo.TaskEntityType
	DROP	CONSTRAINT	UQ_TaskEntityType_Name
END
GO

ALTER TABLE dbo.TaskEntityType
ADD CONSTRAINT UQ_TaskEntityType_Name UNIQUE NONCLUSTERED
(
	Name
)
GO
